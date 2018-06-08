using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FantasticTour.Repository
{
    public interface IUnitOfWork
    {
        //IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
    }
}