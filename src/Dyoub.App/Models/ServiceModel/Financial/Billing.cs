// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class Billing
    {
        private TenantContext context;

        public Billing(TenantContext context)
        {
            this.context = context;
        }

        public async Task<bool> Confirm(SaleOrder saleOrder)
        {
            return false;
        }

        public async Task<bool> Revert(SaleOrder saleOrder)
        {
            return false;
        }
    }
}
