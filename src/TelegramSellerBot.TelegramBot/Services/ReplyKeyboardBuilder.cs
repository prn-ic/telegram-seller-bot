using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramSellerBot.TelegramBot.Services
{
    public class ReplyKeyboardBuilder
    {
        public static ReplyKeyboardMarkup GenerateBasicReplyKeyboard()
        {
            var replyMarkup = new ReplyKeyboardMarkup(true).AddNewRow(
                "👤 Личный кабинет 👤",
                "💼 Сервисы 💼"
            );

            return replyMarkup;
        }
    }
}
