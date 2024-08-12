using Microsoft.EntityFrameworkCore;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class SubscriptionRepository
        : GeneralRepository<Subscription, Guid>,
            ISubscriptionRepository
    {
        public SubscriptionRepository(AppDbContext context)
            : base(context) { }

        public override async Task<Subscription?> GetAsync(
            Guid id,
            CancellationToken cancellationToken = default
        )
        {
            return await _context
                .Subscriptions.Include(x => x.Service)
                .Include(x => x.Status)
                .Include(x => x.Duration)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public override async Task<IEnumerable<Subscription>> GetAsync(
            CancellationToken cancellationToken = default
        )
        {
            return await _context
                .Subscriptions.Include(x => x.Service)
                .Include(x => x.Status)
                .Include(x => x.Duration)
                .ToListAsync(cancellationToken);
        }
    }
}
