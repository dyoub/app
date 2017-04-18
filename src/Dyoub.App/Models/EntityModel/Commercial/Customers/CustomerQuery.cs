// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.Customers
{
    public static class CustomerQuery
    {
        public static IQueryable<Customer> OrderedByName(this IQueryable<Customer> customers)
        {
            return customers.OrderBy(customer => customer.Name);
        }

        public static IQueryable<Customer> WhereId(this IQueryable<Customer> customers, int id)
        {
            return customers.Where(customer => customer.Id == id);
        }

        public static IQueryable<Customer> WhereNameContains(this IQueryable<Customer> customers, params string[] words)
        {
            foreach (string word in words)
            {
                customers = customers.Where(customer => customer.Name.Contains(word));
            }

            return customers;
        }
    }
}
