using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotDurationAvailabilityService
    {
        Task<TelegramBotDurationAvailabilityDto> AddAsync(
            CreateTelegramBotDurationAvailabilityRequest request,
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TelegramBotDurationAvailabilityDto>> GetAsync(
            Guid serviceId,
            CancellationToken cancellationToken = default
        );

        Task<TelegramBotDurationAvailabilityDto> GetAsync(
            int id,
            CancellationToken cancellationToken = default
        );

        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
        Task<TelegramBotDurationAvailabilityDto> GetAsync(
            Guid serviceId,
            int durationId,
            CancellationToken cancellationToken = default
        );
    }
}
