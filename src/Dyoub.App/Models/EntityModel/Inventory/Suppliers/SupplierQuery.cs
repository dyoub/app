// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.Suppliers
{
    public static class SupplierQuery
    {
        public static IQueryable<Supplier> OrderedByName(this IQueryable<Supplier> customers)
        {
            return customers.OrderBy(customer => customer.Name);
        }

        public static IQueryable<Supplier> WhereId(this IQueryable<Supplier> customers, int id)
        {
            return customers.Where(customer => customer.Id == id);
        }

        public static IQueryable<Supplier> WhereNameContains(this IQueryable<Supplier> customers, params string[] words)
        {
            foreach (string word in words)
            {
                customers = customers.Where(customer => customer.Name.Contains(word));
            }

            return customers;
        }
    }
}
