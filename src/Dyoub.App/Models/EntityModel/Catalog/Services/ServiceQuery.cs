// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Catalog.Services
{
    public static class ServiceQuery
    {
        public static IQueryable<ItemPrice> AsItemPrice(this IQueryable<Service> services, int storeId)
        {
            return services.Select(service => new ItemPrice
            {
                TenantId = service.TenantId,
                StoreId = storeId,
                ProductId = null,
                ServiceId = service.Id,
                Name = service.Name,
                Code = service.Code,
                Marketed = service.Marketed,
                CanFraction = service.CanFraction,
                UnitPrice = service.ServicePrices
                    .Where(servicePrice => servicePrice.StoreId == storeId)
                    .Select(servicePrice => (decimal?)servicePrice.UnitPrice)
                    .FirstOrDefault()
            });
        }

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
