// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.ItemPrices
{
    public static class ItemPriceQuery
    {
        public static IQueryable<ItemPrice> ForSaleItems(this IQueryable<ItemPrice> itemPrices, IEnumerable<SaleItem> saleItems)
        {
            IEnumerable<int> productIds = saleItems.Where(item => item.IsProduct).Select(item => item.Id);
            IEnumerable<int> serviceIds = saleItems.Where(item => item.IsService).Select(item => item.Id);

            return itemPrices.Where(itemPrice =>
                productIds.Contains(itemPrice.ProductId.Value) ||
                serviceIds.Contains(itemPrice.ServiceId.Value));
        }

        public static IQueryable<ItemPrice> OrderedByName(this IQueryable<ItemPrice> items)
        {
            return items.OrderBy(item => item.Name);
        }

        public static IQueryable<ItemPrice> WhereItemMarketed(this IQueryable<ItemPrice> itemPrices)
        {
            return itemPrices.Where(itemPrice => itemPrice.Marketed);
        }

        public static IQueryable<ItemPrice> WhereNameOrCode(this IQueryable<ItemPrice> items, params string[] words)
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

        public static IQueryable<ItemPrice> WhereStoreId(this IQueryable<ItemPrice> itemPrices, int storeId)
        {
            return itemPrices.Where(itemPrice => itemPrice.StoreId == storeId);
        }
    }
}
