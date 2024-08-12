using Telegram.Bot.Types;

namespace TelegramSellerBot.TelegramBot.Contracts
{
    public interface ICallbackQueryProcessorContract
    {
        Task<Message> Process(CallbackQuery callbackQuery);
    }
}