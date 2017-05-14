// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;

namespace Dyoub.App.Models.ServiceModel.Inventory
{
    public class ProductConsumption
    {
        public TenantContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }

        public ProductConsumption(TenantContext tenant, SaleOrder saleOrder)
        {
            Tenant = tenant;
            SaleOrder = saleOrder;
        }

        public void Confirm()
        {
        }

        public void Revert()
        {
        }
    }
}
