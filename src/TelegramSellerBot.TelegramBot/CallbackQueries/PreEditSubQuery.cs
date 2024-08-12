using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Common;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Requests;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class PreEditSubQuery : ICallbackQueryProcessorContract
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ITelegramBotDurationAvailabilityService _telegramBotDurationAvailabilityService;
        private readonly ITelegramBotClient _bot;

        public PreEditSubQuery(
            ISubscriptionService subscriptionService,
            ITelegramBotClient bot,
            ITelegramBotDurationAvailabilityService telegramBotDurationAvailabilityService
        )
        {
            _subscriptionService = subscriptionService;
            _bot = bot;
            _telegramBotDurationAvailabilityService = telegramBotDurationAvailabilityService;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid subscriptionId = Guid.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);

            SubscriptionDto? subscription = await _subscriptionService.GetAsync(subscriptionId);
            var availabilities = await _telegramBotDurationAvailabilityService.GetAsync(subscription.Service!.Id);

            InlineKeyboardMarkup markup = new InlineKeyboardMarkup().GenerateSubscribeDurationAvailabilities(
                availabilities,
                subscriptionId,
                typeof(EditSubQuery)
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                "Выбор периода для подписки на сервис: " + subscription.Service!.Name,
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
