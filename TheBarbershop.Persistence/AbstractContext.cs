using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TheBarbershop.Core.Infrastructure;

namespace TheBarbershop.Persistence
{
    public abstract class AbstractContext : DbContext, IDataContext
    {
        protected AbstractContext() : base() { }
        protected AbstractContext(DbContextOptions options) : base(options) { }

        IQueryable<TEntity> IDataContext.Set<TEntity>() => Set<TEntity>();

        IQueryable<TEntity> IDataContext.NoTrackingSet<TEntity>() => Set<TEntity>().AsNoTracking();

        IEnumerable<TEntity> IDataContext.AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            try
            {
                ChangeTracker.AutoDetectChangesEnabled = false;
                var result = new TEntity[entities.Count()];
                var enumerator = entities.GetEnumerator();
                var index = 0;

                while (enumerator.MoveNext())
                {
                    result[index++] = Add(enumerator.Current).Entity;
                }

                return result;
            }
            finally
            {
                ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        TEntity IDataContext.Add<TEntity>(TEntity entity)
        {
            return Add(entity).Entity;
        }

        TEntity IDataContext.Update<TEntity>(TEntity entity)
        {
            return Update(entity).Entity;
        }
    }
}
