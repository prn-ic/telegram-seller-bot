using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotDurationAvailabilityService
    {
        Task<TelegramBotDurationAvailabilityDto> AddAsync(
            CreateTelegramBotDurationAvailabilityRequest request,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
