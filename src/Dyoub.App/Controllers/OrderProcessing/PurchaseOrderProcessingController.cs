// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.OrderProcessing;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchaseOrders;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.OrderProcessing
{
    public class PurchaseOrderProcessingController : TenantController
    {
        public PurchaseOrderProcessingController() { }

        public PurchaseOrderProcessingController(TenantContext tenant) : base(tenant) { }

        [HttpPost, Route("purchase-orders/confirm"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Confirm(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrderProcessing processing = new PurchaseOrderProcessing(Tenant);

            if (!await processing.Confirm(viewModel.Id.Value))
            {
                if (processing.PurchaseOrder == null)
                    return this.Error("Purchase order not found.");

                if (processing.PurchaseOrder.Confirmed)
                    return this.Error("Purchase order already confirmed.");

                if (processing.HasNoItems)
                    return this.Error("Cannot confirm purchase order without items.");

                if (processing.HasPendingPayment)
                    return this.Error("Cannot confirm purchase order with pending payment.");

                if (processing.ProductReplenishment.HasProductThatNotMovementStock)
                    return this.Error("One or more products do not movement stock.");
            }

            return new PurchaseOrderJson(processing.PurchaseOrder);
        }

        [HttpPost, Route("purchase-orders/revert"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Revert(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrderProcessing processing = new PurchaseOrderProcessing(Tenant);

            if (!await processing.Revert(viewModel.Id.Value))
            {
                if (processing.PurchaseOrder == null)
                {
                    return this.Error("Purchase order not found.");
                }

                if (!processing.PurchaseOrder.Confirmed)
                {
                    return this.Error("Cannot revert purchase order not confirmed.");
                }
            }

            return new PurchaseOrderJson(processing.PurchaseOrder);
        }
    }
}
