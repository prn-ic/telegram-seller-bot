using Microsoft.EntityFrameworkCore;
using TelegramSellerBot.Core.Entities;
using TelegramSellerBot.Core.Exceptions;
using TelegramSellerBot.Core.Repositories;
using TelegramSellerBot.Persistense.Data;

namespace TelegramSellerBot.Persistense.Repositories
{
    public abstract class GeneralRepository<T, TId> : IGeneralRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : struct
    {
        protected readonly AppDbContext _context;

        protected GeneralRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var result = await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task DeleteAsync(TId id, CancellationToken cancellationToken = default)
        {
            T? entity = await _context
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

            if (entity is null)
            {
                throw new InvalidRequestException(
                    string.Format("{0} wasn't found", typeof(T).Name)
                );
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(TId id, CancellationToken cancellationToken = default)
        {
            return await _context
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);;
        }

        public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            T? entityFromDatabase = await _context
                .Set<T>()
                .FirstOrDefaultAsync(x => x.Id.Equals(entity.Id), cancellationToken);

            if (entity is null)
            {
                throw new InvalidRequestException(
                    string.Format("{0} wasn't found", typeof(T).Name)
                );
            }

            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
