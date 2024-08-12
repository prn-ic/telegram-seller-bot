using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class SubscribeQuery : ICallbackQueryProcessorContract
    {
        private readonly ITelegramBotDurationAvailabilityService _durationAvailabilityService;
        private readonly ITelegramBotClient _bot;

        public SubscribeQuery(
            ITelegramBotClient telegramBotClient,
            ITelegramBotDurationAvailabilityService durationAvailabilityService
        )
        {
            _bot = telegramBotClient;
            _durationAvailabilityService = durationAvailabilityService;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            Guid botId = Guid.Parse(callbackQuery.Data.Split("_")[1].Split("=")[1]);
            var availabilities = await _durationAvailabilityService.GetAsync(botId);
            var markup = new InlineKeyboardMarkup().GenerateBotDurationAvailabilities(
                availabilities,
                typeof(AddSubQuery)
            );

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                "Выбор периода для подписки на сервис: ",
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
