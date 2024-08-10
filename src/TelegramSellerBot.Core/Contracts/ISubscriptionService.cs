using TelegramSellerBot.Core.Dtos;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ISubscriptionService
    {
        Task<SubscriptionDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SubscriptionDto>> GetAsync(
            string userId,
            CancellationToken cancellationToken = default
        );
        Task<SubscriptionDto> CreateAsync(
            CreateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        );
        Task<SubscriptionDto> UpdateAsync(
            UpdateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        );
        Task DeclineAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
    }
}
