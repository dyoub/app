// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Inventory.PurchasePayments;
using Dyoub.App.Models.ViewModel.Inventory.PurchasePayments;
using Dyoub.App.Models.ServiceModel.Inventory;

namespace Dyoub.App.Controllers.Inventory
{
    public class PurchasePaymentsController : TenantController
    {
        public PurchasePaymentsController() { }

        public PurchasePaymentsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("purchase-orders/details/{id:int}/payments"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/PurchasePayments/PurchasePaymentsDetails.cshtml");
        }

        [HttpGet, Route("purchase-orders/edit/{id:int}/payments"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Inventory/PurchasePayments/PurchasePaymentsEdit.cshtml");
        }

        [HttpPost, Route("purchase-orders/payments"), Authorization(Scope = "purchase-orders.read")]
        public async Task<ActionResult> List(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .IncludePurchasePayments()
                .SingleOrDefaultAsync();

            return new PurchasePaymentListJson(purchaseOrder);
        }

        [HttpPost, Route("purchase-orders/payments/update"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Update(UpdatePurchasePaymentsViewModel viewModel)
        {
            PurchasePromisedPayments promisedPayments = new PurchasePromisedPayments(Tenant);

            bool registered = await promisedPayments.RegisterPayments(
                viewModel.PurchaseOrderId.Value,
                viewModel.MapPurchasePayments(),
                viewModel.Discount
            );

            if (!registered)
            {
                if (promisedPayments.PurchaseOrder == null)
                {
                    return this.Error("Purchase order not found.");
                }

                if (promisedPayments.PurchaseOrder.Confirmed)
                {
                    return this.Error("Purchase order already confirmed.");
                }

                if (promisedPayments.HasPendingPayment)
                {
                    return this.Error("Purchase order has pending payment.");
                }
            }

            return this.Success();
        }
    }
}
