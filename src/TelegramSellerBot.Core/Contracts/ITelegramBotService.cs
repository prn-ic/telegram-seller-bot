using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotService
    {
        Task<TelegramBotDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TelegramBotDto>> GetAsync(CancellationToken cancellationToken = default);
    }
}
