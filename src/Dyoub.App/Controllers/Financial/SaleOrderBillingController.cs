// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleOrders;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Financial
{
    public class SaleOrderBillingController : TenantController
    {
        public SaleOrderBillingController() { }

        public SaleOrderBillingController(TenantContext tenant) : base(tenant) { }
        
        [HttpPost, Route("sale-orders/confirm"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Confirm(SaleOrderIdViewModel viewModel)
        {
            Billing billing = new Billing(Tenant);

            if (!await billing.Confirm(viewModel.Id.Value))
            {
                if (billing.SaleOrder == null)
                {
                    return this.Error("Sale order not found.");
                }

                if (billing.SaleOrder.Confirmed)
                {
                    return this.Error("Sale order already confirmed.");
                }

                if (billing.HasNoItems)
                {
                    return this.Error("Cannot confirm sale order without items.");
                }

                if (billing.HasPendingPayment)
                {
                    return this.Error("Cannot confirm sale order with pending payment.");
                }
            }

            return new SaleOrderJson(billing.SaleOrder);
        }

        [HttpPost, Route("sale-orders/revert"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Revert(SaleOrderIdViewModel viewModel)
        {
            Billing billing = new Billing(Tenant);

            if (!await billing.Revert(viewModel.Id.Value))
            {
                if (billing.SaleOrder == null)
                {
                    return this.Error("Sale order not found.");
                }

                if (!billing.SaleOrder.Confirmed)
                {
                    return this.Error("Cannot revert sale order not confirmed.");
                }
            }

            return new SaleOrderJson(billing.SaleOrder);
        }
    }
}
