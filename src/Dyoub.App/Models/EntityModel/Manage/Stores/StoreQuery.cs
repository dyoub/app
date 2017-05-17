// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Manage.Stores
{
    public static class StoreQuery
    {
        public static IQueryable<Store> IncludePrices(this IQueryable<Store> stores)
        {
            return stores
                .Include(store => store.ProductPrices)
                .Include(store => store.ServicePrices);
        }

        public static IQueryable<Store> OrderByName(this IQueryable<Store> stores)
        {
            return stores.OrderBy(store => store.Name);
        }

        public static IQueryable<Store> WhereId(this IQueryable<Store> stores, int id)
        {
            return stores.Where(store => store.Id == id);
        }

        public static IQueryable<Store> WhereNameContains(this IQueryable<Store> stores, params string[] words)
        {
            foreach (string word in words)
            {
                stores = stores.Where(store => store.Name.Contains(word));
            }

            return stores;
        }
    }
}
