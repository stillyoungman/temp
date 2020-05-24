using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheBarbershop.Core.Infrastructure
{
    public interface IDataContext:  IDisposable, IAsyncDisposable
    {
        //transaction

        IQueryable<TEntity> NoTrackingSet<TEntity>() where TEntity : class;
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        TEntity Add<TEntity>(TEntity entity) where TEntity: class;
        IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
