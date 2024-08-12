using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramSellerBot.TelegramBot.Contracts;

namespace TelegramSellerBot.TelegramBot.CallbackQueries
{
    public class GetHelpQuery : ICallbackQueryProcessorContract
    {
        private readonly ITelegramBotClient _bot;
        public GetHelpQuery(ITelegramBotClient telegramBotClient)
        {
            _bot = telegramBotClient;
        }
        public async Task<Message> Process(CallbackQuery callbackQuery)
        {
            const string help = """
                Привет, это бот, для управления подписками на различные сервисы (телеграм боты).
                Для продолжения пользуйся меню ниже
                """;

            return await _bot.SendTextMessageAsync(
                callbackQuery.Message!.Chat,
                help,
                parseMode: ParseMode.Html
            );
        }
    }
}
