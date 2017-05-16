// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.OrderProcessing;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleOrders;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.OrderProcessing
{
    public class SaleOrderProcessingController : TenantController
    {
        public SaleOrderProcessingController() { }

        public SaleOrderProcessingController(TenantContext tenant) : base(tenant) { }
        
        [HttpPost, Route("sale-orders/confirm"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Confirm(SaleOrderIdViewModel viewModel)
        {
            SaleOrderProcessing processing = new SaleOrderProcessing(Tenant);
            
            if (!await processing.Confirm(viewModel.Id.Value))
            {
                if (processing.SaleOrder == null)
                    return this.Error("Sale order not found.");

                if (processing.SaleOrder.Confirmed)
                    return this.Error("Sale order already confirmed.");

                if (processing.HasNoItems)
                    return this.Error("Cannot confirm sale order without items.");

                if (processing.HasPendingPayment)
                    return this.Error("Cannot confirm sale order with pending payment.");

                if (processing.ProductConsumption.InsufficientBalance)
                    return this.Error("One or more products with insufficient balance.");
            }

            return new SaleOrderJson(processing.SaleOrder);
        }

        [HttpPost, Route("sale-orders/revert"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Revert(SaleOrderIdViewModel viewModel)
        {
            SaleOrderProcessing processing = new SaleOrderProcessing(Tenant);

            if (!await processing.Revert(viewModel.Id.Value))
            {
                if (processing.SaleOrder == null)
                {
                    return this.Error("Sale order not found.");
                }

                if (!processing.SaleOrder.Confirmed)
                {
                    return this.Error("Cannot revert sale order not confirmed.");
                }
            }

            return new SaleOrderJson(processing.SaleOrder);
        }
    }
}
