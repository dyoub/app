// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;

namespace Dyoub.Test.Factories.Commercial
{
    public class SaleServiceFactory
    {
        public static SaleService SaleService(SaleOrder saleOrder, Service service)
        {
            return new SaleService
            {
                SaleOrder = saleOrder,
                Service = service,
                Tenant = saleOrder.Tenant,
                Quantity = 1,
                UnitPrice = 10.90M,
                Total = 10.90M,
                TotalPayable = 10.90M
            };
        }
    }
}
