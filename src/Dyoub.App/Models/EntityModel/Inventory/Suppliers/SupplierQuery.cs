// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.Suppliers
{
    public static class SupplierQuery
    {
        public static IQueryable<Supplier> OrderedByName(this IQueryable<Supplier> suppliers)
        {
            return suppliers.OrderBy(supplier => supplier.Name);
        }

        public static IQueryable<Supplier> WhereId(this IQueryable<Supplier> suppliers, int id)
        {
            return suppliers.Where(supplier => supplier.Id == id);
        }

        public static IQueryable<Supplier> WhereNameContains(this IQueryable<Supplier> suppliers, params string[] words)
        {
            foreach (string word in words)
            {
                suppliers = suppliers.Where(supplier => supplier.Name.Contains(word));
            }

            return suppliers;
        }
    }
}
