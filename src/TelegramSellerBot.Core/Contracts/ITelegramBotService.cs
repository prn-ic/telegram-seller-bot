using TelegramSellerBot.Core.Dtos;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotService
    {
        Task<TelegramBotDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TelegramBotDto>> GetAsync(CancellationToken cancellationToken = default);
    }
}
