using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Repositories
{
    public interface ITelegramBotDurationAvailabilityRepository
        : IGeneralRepository<TelegramBotDurationAvailability, int>
    {
        Task<TelegramBotDurationAvailability?> GetAsync(
            Guid botId,
            int durationId,
            CancellationToken cancellationToken = default
        );
    }
}
