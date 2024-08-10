using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ITelegramBotDurationAvailabilityService
    {
        Task<TelegramBotDurationAvailability> AddAsync(
            CreateTelegramBotDurationAvailabilityRequest request,
            CancellationToken cancellationToken = default);

        Task RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
