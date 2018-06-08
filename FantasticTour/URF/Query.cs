using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Repository;
using Microsoft.EntityFrameworkCore;

namespace FantasticTour.URF
{
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class
    {
        private int? _skip;
        private int? _take;
        private IQueryable<TEntity> _query;
        private IOrderedQueryable<TEntity> _orderedQuery;

        public Query(IRepository<TEntity> repository) => _query = repository.Queryable();

        public virtual IQuery<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
            => Set(q => q._query = Queryable.Where(q._query, predicate));

        public virtual IQuery<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty)
            => Set(q => q._query = EntityFrameworkQueryableExtensions.Include(q._query, navigationProperty));

        public virtual IQuery<TEntity> Include(string navigationPropertyPath)
            => Set(q => q._query = q._query.Include(navigationPropertyPath));

        public virtual IQuery<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector)
        {
            if (_orderedQuery == null) _orderedQuery = Queryable.OrderBy(_query, keySelector);
            else Queryable.OrderBy(_orderedQuery, keySelector);
            return this;
        }

        public virtual IQuery<TEntity> ThenBy(Expression<Func<TEntity, object>> thenBy)
            => Set(q => Queryable.ThenBy(q._orderedQuery, thenBy));

        public virtual IQuery<TEntity> OrderByDescending(Expression<Func<TEntity, object>> keySelector)
        {
            if (_orderedQuery == null) _orderedQuery = Queryable.OrderByDescending(_query, keySelector);
            else Queryable.OrderByDescending(_orderedQuery, keySelector);
            return this;
        }

        public virtual IQuery<TEntity> ThenByDescending(Expression<Func<TEntity, object>> thenByDescending)
            => Set(q => Queryable.ThenByDescending(q._orderedQuery, thenByDescending));

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = new CancellationToken())
            => await _query.CountAsync(cancellationToken);

        public virtual IQuery<TEntity> Skip(int skip)
            => Set(q => q._skip = skip);

        public virtual IQuery<TEntity> Take(int take)
            => Set(q => q._take = take);

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            _query = _orderedQuery ?? _query;

            if (_skip.HasValue) _query = Queryable.Skip(_query, _skip.Value);
            if (_take.HasValue) _query = Queryable.Take(_query, _take.Value);

            return await EntityFrameworkQueryableExtensions.ToListAsync(_query, cancellationToken);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(_query, predicate, cancellationToken);

        public virtual async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_query, predicate, cancellationToken);

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await EntityFrameworkQueryableExtensions.AnyAsync(_query, predicate, cancellationToken);

        public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken)
            => await _query.AnyAsync(cancellationToken);

        public virtual async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
            => await EntityFrameworkQueryableExtensions.AllAsync(_query, predicate, cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object[] parameters, CancellationToken cancellationToken = new CancellationToken())
            => await RelationalQueryableExtensions.FromSql(_query, sql, parameters).ToListAsync(cancellationToken);

        private IQuery<TEntity> Set(Action<Query<TEntity>> setParameter)
        {
            setParameter(this);
            return this;
        }
    }
}