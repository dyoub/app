// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class PurchaseCost
    {
        public TenantContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }

        public PurchaseCost(TenantContext tenant, PurchaseOrder saleOrder)
        {
            Tenant = tenant;
            PurchaseOrder = saleOrder;
        }

        public void Confirm()
        {
            // TODO
        }

        public void Revert()
        {
            // TODO
        }
    }
}
