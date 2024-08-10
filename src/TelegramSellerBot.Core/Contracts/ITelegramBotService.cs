using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotService
    {
        Task<TelegramBot> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TelegramBot>> GetAsync(CancellationToken cancellationToken = default);
    }
}
