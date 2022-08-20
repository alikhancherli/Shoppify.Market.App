using Microsoft.EntityFrameworkCore;
using Shoppify.Market.App.Domain.Entites;

namespace Shoppify.Market.App.Persistence.Repositories.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IRootEntity
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> TableAsTracking { get; }
        IQueryable<TEntity> TableAsNoTracking { get; }

        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
        ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true);
    }
}
