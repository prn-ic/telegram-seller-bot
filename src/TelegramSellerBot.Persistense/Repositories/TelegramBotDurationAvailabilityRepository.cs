using Microsoft.EntityFrameworkCore;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public class TelegramBotDurationAvailabilityRepository
        : GeneralRepository<TelegramBotDurationAvailability, int>,
            ITelegramBotDurationAvailabilityRepository
    {
        public TelegramBotDurationAvailabilityRepository(AppDbContext context)
            : base(context) { }

        public override async Task<IEnumerable<TelegramBotDurationAvailability>> GetAsync(
            CancellationToken cancellationToken = default
        )
        {
            return await _context
                .TelegramBotDurationAvailabilities.Include(x => x.TelegramBot)
                .Include(x => x.Duration)
                .ToListAsync(cancellationToken);
        }

        public override async Task<TelegramBotDurationAvailability?> GetAsync(
            int id,
            CancellationToken cancellationToken = default
        )
        {
            return await _context
                .TelegramBotDurationAvailabilities.Include(x => x.TelegramBot)
                .Include(x => x.Duration)
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<TelegramBotDurationAvailability?> GetAsync(
            Guid botId,
            int durationId,
            CancellationToken cancellationToken = default
        )
        {
            return await _context
                .TelegramBotDurationAvailabilities.Include(x => x.TelegramBot)
                .Include(x => x.Duration)
                .FirstOrDefaultAsync(
                    x => (int)x.DurationId == durationId && x.TelegramBot!.Id == botId,
                    cancellationToken
                );
        }
    }
}
