using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.TelegramBot.CallbackQueries;
using TelegramSellerBot.TelegramBot.Contracts;

namespace TelegramSellerBot.TelegramBot.Services
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ILogger<UpdateHandler> _logger;
        private readonly ITelegramBotClient _bot;
        private readonly ITelegramBotService _telegramBotService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISubscriptionService _subscriptionService;

        public UpdateHandler(
            ITelegramBotClient bot,
            ILogger<UpdateHandler> logger,
            ITelegramBotService telegramBotService,
            IServiceProvider serviceProvider,
            ISubscriptionService subscriptionService
        )
        {
            _bot = bot;
            _logger = logger;
            _telegramBotService = telegramBotService;
            _serviceProvider = serviceProvider;
            _subscriptionService = subscriptionService;
        }

        public async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (update.Type == UpdateType.Message)
                await OnMessage(update.Message!, cancellationToken);

            if (update.Type == UpdateType.CallbackQuery)
                await OnCallbackQuery(update.CallbackQuery!);
        }

        private async Task OnMessage(Message message, CancellationToken cancellationToken = default)
        {
            switch (message.Text)
            {
                case "/start":
                    await GetHelp(message, cancellationToken);
                    break;
                case "💼 Сервисы 💼":
                    await GetAllServices(message, cancellationToken: cancellationToken);
                    break;
                case "👤 Личный кабинет 👤":
                    await GetMy(message, cancellationToken: cancellationToken);
                    break;
                default:
                    await GetHelp(message, cancellationToken);
                    break;
            }
        }

        public async Task<Message> GetMy(
            Message message,
            CancellationToken cancellationToken = default
        )
        {
            const string lk = "Личный кабинет";

            var subscriptions = await _subscriptionService.GetAsync(
                message.From!.Id.ToString(),
                cancellationToken
            );
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup().GenerateMySubscriptionsMenu(
                subscriptions,
                typeof(ManageBotQuery)
            );

            return await _bot.SendTextMessageAsync(
                message.Chat,
                lk,
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }

        public async Task<Message> GetHelp(Message message, CancellationToken cancellationToken)
        {
            const string help = """
                Привет, это бот, для управления подписками на различные сервисы (телеграм боты).
                Для продолжения пользуйся меню ниже
                """;

            return await _bot.SendTextMessageAsync(
                message.Chat,
                help,
                parseMode: ParseMode.Html,
                replyMarkup: GenerateBasicReplyKeyboard()
            );
        }

        public async Task<Message> GetAllServices(
            Message message,
            int skip = 0,
            int take = 5,
            CancellationToken cancellationToken = default
        )
        {
            var telegramBots = await _telegramBotService.GetAsync(
                skip,
                take,
                cancellationToken: cancellationToken
            );

            if (telegramBots.Count() == 0)
                return await _bot.SendTextMessageAsync(
                    message.Chat,
                    "Сервисов нет!",
                    parseMode: ParseMode.Html
                );

            return await _bot.SendTextMessageAsync(
                message.Chat,
                "Есть следующие сервисы:",
                parseMode: ParseMode.Html,
                replyMarkup: new InlineKeyboardMarkup()
                    .GenerateServicePreviewWithPagination(
                        telegramBots,
                        skip,
                        take,
                        typeof(GetRangeQuery)
                    )
                    .GenerateGoBackButton(typeof(GetHelpQuery), "")
            );
        }

        public ReplyKeyboardMarkup GenerateBasicReplyKeyboard()
        {
            var replyMarkup = new ReplyKeyboardMarkup(true).AddNewRow(
                "👤 Личный кабинет 👤",
                "💼 Сервисы 💼"
            );

            return replyMarkup;
        }

        private async Task OnCallbackQuery(CallbackQuery callbackQuery)
        {
            string callbackName = callbackQuery.Data is not null
                ? callbackQuery.Data.Split("_")[0]
                : "";

            using var scope = _serviceProvider.CreateScope();
            string assemblyNamespase = "TelegramSellerBot.TelegramBot.CallbackQueries.";
            Type type =
                Type.GetType(assemblyNamespase + callbackName)
                ?? throw new InvalidDataException(
                    "The callback wasn't found: "
                        + callbackName
                        + " with assembly "
                        + assemblyNamespase
                );
            var updateHandler =
                scope.ServiceProvider.GetRequiredService(type) as ICallbackQueryProcessorContract;

            if (updateHandler is null)
                throw new InvalidDataException(
                    "The callback wasn't found: "
                        + callbackName
                        + " with assembly "
                        + assemblyNamespase
                );

            await updateHandler.Process(callbackQuery);
            await _bot.AnswerCallbackQueryAsync(callbackQuery.Id, $"Received {callbackQuery.Data}");
            await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                $"Received {callbackQuery.Data}"
            );
        }

        public Task HandleErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            HandleErrorSource source,
            CancellationToken cancellationToken
        )
        {
            _logger.LogError(exception.Message);
            _logger.LogError(exception.StackTrace);

            return Task.CompletedTask;
        }
    }
}
