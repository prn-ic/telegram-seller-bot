using TelegramSellerBot.Core.Entities;

namespace TelegramSellerBot.Core.Repositories
{
    public interface IGeneralRepository<T, TId> where T: BaseEntity<TId> where TId: struct
    {
        Task<T> GetAsync(TId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
    }
}