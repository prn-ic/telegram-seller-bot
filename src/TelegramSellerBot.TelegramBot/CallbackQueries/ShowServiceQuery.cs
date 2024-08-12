using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class ShowServiceQuery : ICallbackQueryProcessorContract
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITelegramBotClient _bot;

        public ShowServiceQuery(
            ITelegramBotClient bot,
            ITelegramBotService telegramBotService,
            ISubscriptionService subscriptionService
        )
        {
            _bot = bot;
            _telegramBotService = telegramBotService;
            _subscriptionService = subscriptionService;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid botId = Guid.Parse(callbackQuery.Data.Split("_")[1]);
            var bot = await _telegramBotService.GetAsync(botId);
            string serviceInfo = string.Format(
                """
                    Имя: {0}
                Описание: {1}
                """,
                bot.Name,
                bot.Description
            );

            var inlineMarkup = new InlineKeyboardMarkup();

            var userSubscriptions = await _subscriptionService.GetAsync(
                callbackQuery.From.Id.ToString()
            );

            var subscription = userSubscriptions.FirstOrDefault(x => x.Service!.Id == bot.Id);

            if (subscription is not null)
                inlineMarkup.GenerateGoToManage(subscription.Id, typeof(ManageBotQuery));
            else
                inlineMarkup.AddButton(
                    "Оформить подписку",
                    typeof(SubscribeQuery).Name + "_sId=" + bot.Id
                );

            inlineMarkup.GenerateGoBackButton(typeof(GetRangeQuery), "skip=0_take=5");

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                serviceInfo,
                parseMode: ParseMode.Html,
                replyMarkup: inlineMarkup
            );
        }
    }
}
