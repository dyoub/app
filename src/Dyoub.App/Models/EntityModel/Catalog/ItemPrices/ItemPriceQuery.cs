// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.ItemPrices
{
    public static class ItemPriceQuery
    {
        public static IQueryable<ItemPrice> OrderedByName(this IQueryable<ItemPrice> items)
        {
            return items.OrderBy(item => item.Name);
        }

        public static IQueryable<ItemPrice> WherwCode(this IQueryable<ItemPrice> items, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return items;
            }

            return items = items.Where(item => item.Code == code);
        }

        public static IQueryable<ItemPrice> WhereCodeOrName(this IQueryable<ItemPrice> items, params string[] words)
        {
            if (words.Count() == 1)
            {
                string word = words.First();
                return items.Where(item => item.Code == word || item.Name.Contains(word));
            }

            foreach (string word in words)
            {
                items = items.Where(item => item.Name.Contains(word));
            }

            return items;
        }

        public static IQueryable<ItemPrice> WhereItemMarketed(this IQueryable<ItemPrice> itemPrices)
        {
            return itemPrices.Where(itemPrice => itemPrice.Marketed);
        }

        public static IQueryable<ItemPrice> WhereNameContains(this IQueryable<ItemPrice> items, params string[] words)
        {
            foreach (string word in words)
            {
                items = items.Where(item => item.Name.Contains(word));
            }

            return items;
        }

        public static IQueryable<ItemPrice> WhereProductIdIn(this IQueryable<ItemPrice> itemPrices, IEnumerable<int> productIds)
        {
            return itemPrices.Where(itemPrice => productIds.Contains(itemPrice.Id));
        }

        public static IQueryable<ItemPrice> WhereServiceIdIn(this IQueryable<ItemPrice> itemPrices, IEnumerable<int> serviceIds)
        {
            return itemPrices.Where(itemPrice => serviceIds.Contains(itemPrice.Id));
        }

        public static IQueryable<ItemPrice> WhereStoreId(this IQueryable<ItemPrice> itemPrices, int storeId)
        {
            return itemPrices.Where(itemPrice => itemPrice.StoreId == storeId);
        }
    }
}
