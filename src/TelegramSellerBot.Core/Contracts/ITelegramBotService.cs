using TelegramSellerBot.Core.Dtos;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotService
    {
        Task<TelegramBotDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TelegramBotDto> AddAsync(
            TelegramBotDto telegramBotDto,
            CancellationToken cancellationToken = default
        );
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TelegramBotDto>> GetAsync(
            int skip = 0,
            int take = int.MaxValue,
            CancellationToken cancellationToken = default
        );
    }
}
