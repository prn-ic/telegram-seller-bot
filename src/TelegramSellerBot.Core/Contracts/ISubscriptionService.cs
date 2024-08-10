using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Requests;

namespace TelegramSellerBot.Core.Contracts
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Subscription>> GetAsync(
            string userId,
            CancellationToken cancellationToken = default
        );
        Task<Subscription> CreateAsync(
            CreateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        );
        Task<Subscription> UpdateAsync(
            UpdateSubscriptionRequest request,
            CancellationToken cancellationToken = default
        );
        Task DeclineAsync(Guid subscriptionId, CancellationToken cancellationToken = default);
    }
}
