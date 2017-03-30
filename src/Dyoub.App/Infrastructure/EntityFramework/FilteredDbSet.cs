// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Dyoub.App.Infrastructure.EntityFramework
{
    public class FilteredDbSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity>,
        IQueryable<TEntity>, IQueryable, IDbAsyncEnumerable<TEntity>,
        IEnumerable<TEntity>, IEnumerable where TEntity : class
    {
        private readonly DbSet<TEntity> Set;
        private readonly IQueryable<TEntity> FilteredSet;

        public Expression Expression
        {
            get { return FilteredSet.Expression; }
        }

        public Type ElementType
        {
            get { return typeof(TEntity); }
        }

        public IQueryProvider Provider
        {
            get { return FilteredSet.Provider; }
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return Set.Local; }
        }

        public FilteredDbSet(DbSet<TEntity> dbSet, Expression<Func<TEntity, bool>> filter)
        {
            Set = dbSet;
            FilteredSet = dbSet.Where(filter);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return FilteredSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return FilteredSet.GetEnumerator();
        }

        public IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
        {
            return (FilteredSet as IDbAsyncEnumerable<TEntity>).GetAsyncEnumerator();
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return (FilteredSet as IDbAsyncEnumerable).GetAsyncEnumerator();
        }

        public override TEntity Find(params object[] keyValues)
        {
            return Set.Find(keyValues);
        }

        public override TEntity Add(TEntity entity)
        {
            return Set.Add(entity);
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return Set.AddRange(entities);
        }

        public override TEntity Remove(TEntity entity)
        {
            return Set.Remove(entity);
        }

        public override IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return Set.RemoveRange(entities);
        }

        public override TEntity Attach(TEntity entity)
        {
            return Set.Attach(entity);
        }

        public override TEntity Create()
        {
            return Set.Create();
        }

        TDerivedEntity IDbSet<TEntity>.Create<TDerivedEntity>()
        {
            return Set.Create<TDerivedEntity>();
        }
    }
}
