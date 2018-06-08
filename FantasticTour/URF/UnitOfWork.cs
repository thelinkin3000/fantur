using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Repository;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.URF
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DataContext Context { get; }

        public UnitOfWork(DataContext context)
        {
            Context = context;
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
            => await Context.SaveChangesAsync(cancellationToken);

        public virtual async Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = new CancellationToken())
            => await RelationalDatabaseFacadeExtensions.ExecuteSqlCommandAsync(Context.Database, sql, parameters, cancellationToken);
    }
}