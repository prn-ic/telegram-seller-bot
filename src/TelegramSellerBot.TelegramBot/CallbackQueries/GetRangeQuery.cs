using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramSellerBot.Core.Contracts;
using TelegramSellerBot.TelegramBot.Contracts;
using TelegramSellerBot.TelegramBot.Services;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class GetRangeQuery : ICallbackQueryProcessorContract
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly ITelegramBotClient _bot;

        public GetRangeQuery(ITelegramBotService telegramBotService, ITelegramBotClient bot)
        {
            _telegramBotService = telegramBotService;
            _bot = bot;
        }

        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            InlineKeyboardMarkup markup = new InlineKeyboardMarkup();

            int skip = int.Parse(callbackQuery.Data!.Split("_")[1].Split("=")[1]);
            int take = int.Parse(callbackQuery.Data.Split("_")[2].Split("=")[1]);

            var bots = await _telegramBotService.GetAsync(skip, take);

            markup.GenerateServicePreviewWithPagination(bots, skip, take, typeof(GetRangeQuery));
            markup.GenerateGoBackButton(typeof(GetHelpQuery), "");
            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                "Есть следующие сервисы:",
                parseMode: ParseMode.Html,
                replyMarkup: markup
            );
        }
    }
}
