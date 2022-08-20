using Microsoft.EntityFrameworkCore;
using Shoppify.Market.App.Domain.Entites;
using Shoppify.Market.App.Persistence.EF;
using Shoppify.Market.App.Persistence.Repositories.Contracts;

namespace Shoppify.Market.App.Persistence.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IRootEntity
    {
        protected readonly ApplicationDbContext DbContext;
       
        public DbSet<TEntity> Entities { get; }

        public IQueryable<TEntity> TableAsTracking => Entities;

        public IQueryable<TEntity> TableAsNoTracking => Entities.AsNoTracking();

        public Repository(ApplicationDbContext context)
        {
            DbContext = context;
            Entities = DbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            await DbContext.AddAsync(entity, cancellationToken);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            await DbContext.AddRangeAsync(entities, cancellationToken);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            DbContext.Remove(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            DbContext.RemoveRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async ValueTask<TEntity?> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await Entities.FindAsync(ids, cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            DbContext.Update(entity);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            DbContext.UpdateRange(entities);
            if (saveNow)
                await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
