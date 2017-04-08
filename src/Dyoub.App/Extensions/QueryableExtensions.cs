// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Extensions
{
    public static class QueryableExtensions
    {
        private const int MinLimit = 1;
        private const int MaxLimit = 30;

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, int index) where TEntity : class
        {
            return Paginate(query, index, MaxLimit);
        }

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, int index, int? limit) where TEntity : class
        {
            if (limit != null)
            {
                if (limit < MinLimit)
                {
                    limit = MinLimit;
                }

                if (limit > MaxLimit)
                {
                    limit = MaxLimit;
                }
            }

            return query.Skip(index).Take(index + (limit ?? MaxLimit));
        }
    }
}
