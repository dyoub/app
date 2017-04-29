// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleServices
{
    public static class SaleServiceQuery
    {
        public static IQueryable<SaleItem> AsSaleItem(this IQueryable<SaleService> saleServices)
        {
            return saleServices.Select(saleService => new SaleItem
            {
                Id = saleService.ServiceId,
                IsProduct = false,
                IsService = true,
                Name = saleService.Service.Name,
                Code = saleService.Service.Code,
                CanFraction = saleService.Service.CanFraction,
                Quantity = saleService.Quantity,
                UnitPrice = saleService.UnitPrice,
                Total = saleService.Total,
                Discount = saleService.Discount,
                TotalPayable = saleService.TotalPayable
            });
        }

        public static IQueryable<SaleService> WhereSaleOrderId(this IQueryable<SaleService> saleServices, int saleOrderId)
        {
            return saleServices.Where(saleService => saleService.SaleOrderId == saleOrderId);
        }

        public static IQueryable<SaleService> WhereServiceId(this IQueryable<SaleService> saleServices, int serviceId)
        {
            return saleServices.Where(saleService => saleService.ServiceId == serviceId);
        }
    }
}
