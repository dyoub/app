// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.Services
{
    public static class ServiceQuery
    {
        public static IQueryable<Service> OrderByName(this IQueryable<Service> products)
        {
            return products.OrderBy(product => product.Name);
        }

        public static IQueryable<Service> WhereCode(this IQueryable<Service> products, string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return products;
            }

            return products.Where(product => product.Code == code);
        }

        public static IQueryable<Service> WhereId(this IQueryable<Service> products, int id)
        {
            return products.Where(product => product.Id == id);
        }

        public static IQueryable<Service> WhereNameContains(this IQueryable<Service> products, params string[] words)
        {
            foreach (string word in words)
            {
                products = products.Where(product => product.Name.Contains(word));
            }

            return products;
        }
    }
}
