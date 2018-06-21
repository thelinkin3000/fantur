using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.URF
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Table();
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken);
        void Attach(TEntity item);
        void Detach(TEntity item);
        void Insert(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableSql(string sql, params object[] parameters);
        IQuery<TEntity> Query();
    }
}
