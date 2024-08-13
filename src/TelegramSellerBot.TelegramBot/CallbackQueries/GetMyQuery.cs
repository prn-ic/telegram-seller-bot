using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class GetMyQuery : ICallbackQueryProcessorContract
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITelegramBotClient _bot;

        public GetMyQuery(
            ISubscriptionService subscriptionService,
            ITelegramBotClient telegramBotClient
        )
        {
            _subscriptionService = subscriptionService;
            _bot = telegramBotClient;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            const string lk = "Личный кабинет";

            var subscriptions = await _subscriptionService.GetAsync(
                callbackQuery.From!.Id.ToString()
            );
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup().GenerateMySubscriptionsMenu(
                subscriptions,
                typeof(ManageBotQuery)
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                lk,
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
