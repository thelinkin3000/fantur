﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FantasticTour.Repository;

namespace FantasticTour.URF
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> Repository;

        public Service(IRepository<TEntity> repository)
            => Repository = repository;

        public virtual void Attach(TEntity item)
            => Repository.Attach(item);

        public virtual void Delete(TEntity item)
            => Repository.Delete(item);

        public virtual async Task<bool> DeleteAsync(object[] keyValues, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.DeleteAsync(keyValues, cancellationToken);

        public virtual async Task<bool> DeleteAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.DeleteAsync(keyValue, cancellationToken);

        public virtual void Detach(TEntity item)
            => Repository.Detach(item);

        public virtual async Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.ExistsAsync(keyValues, cancellationToken);

        public virtual async Task<bool> ExistsAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.ExistsAsync(keyValue, cancellationToken);

        public virtual async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.FindAsync(keyValues, cancellationToken);

        public virtual async Task<TEntity> FindAsync<TKey>(TKey keyValue, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.FindAsync(keyValue, cancellationToken);

        public virtual void Insert(TEntity item)
            => Repository.Insert(item);

        public virtual async Task LoadPropertyAsync(TEntity item, Expression<Func<TEntity, object>> property, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.LoadPropertyAsync(item, property, cancellationToken);

        public virtual IQuery<TEntity> Query()
            => Repository.Query();

        public virtual IQueryable<TEntity> Queryable()
            => Repository.Queryable();

        public virtual IQueryable<TEntity> QueryableSql(string sql, params object[] parameters)
            => Repository.QueryableSql(sql, parameters);

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(CancellationToken cancellationToken = new CancellationToken())
            => await Repository.Query().SelectAsync(cancellationToken);

        public virtual async Task<IEnumerable<TEntity>> SelectSqlAsync(string sql, object[] parameters, CancellationToken cancellationToken = new CancellationToken())
            => await Repository.Query().SelectSqlAsync(sql, parameters, cancellationToken);

        public virtual void Update(TEntity item)
            => Repository.Update(item);
    }
}