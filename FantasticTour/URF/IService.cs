using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FantasticTour.URF
{
    public interface IService<TEntity> where TEntity : class
    {
        void Attach(TEntity item);
        void Delete(TEntity item);
        Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        void Detach(TEntity item);
        Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken);
        void Insert(TEntity item);
        Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken);   
        IQuery<TEntity> Query();
        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> QueryableSql(string sql, params object[] parameters);
        Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object[] parameters, CancellationToken cancellationToken);
        void Update(TEntity item);
    }
}