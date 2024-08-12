using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Requests;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class AddSubQuery : ICallbackQueryProcessorContract
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly ITelegramBotDurationAvailabilityService _telegramBotDurationAvailabilityService;
        private readonly ISubscriptionService _subscriptionService;

        private readonly ITelegramBotClient _bot;

        public AddSubQuery(
            ISubscriptionService subscriptionService,
            ITelegramBotDurationAvailabilityService telegramBotDurationAvailabilityService,
            ITelegramBotService telegramBotService,
            ITelegramBotClient bot
        )
        {
            _subscriptionService = subscriptionService;
            _telegramBotDurationAvailabilityService = telegramBotDurationAvailabilityService;
            _telegramBotService = telegramBotService;
            _bot = bot;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid botId = Guid.Parse(callbackQuery.Data!.Split("_")[2].Split("=")[1]);
            int availabilityId = int.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);

            var bot = await _telegramBotService.GetAsync(botId);
            var availability = await _telegramBotDurationAvailabilityService.GetAsync(
                availabilityId
            );
            var userId = callbackQuery.From.Id.ToString();
            var durationId = availability.Duration!.Id;

            CreateSubscriptionRequest request =
                new()
                {
                    TelegramServiceId = bot.Id,
                    TelegramUserId = userId,
                    Duration = durationId
                };

            SubscriptionDto subscriptionDto = await _subscriptionService.CreateAsync(request);

            InlineKeyboardMarkup markup = new InlineKeyboardMarkup().GenerateGoToManage(
                subscriptionDto.Id,
                typeof(ManageBotQuery)
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                "Успешно! Вы арендовали подписку на сервис" + bot.Name,
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
