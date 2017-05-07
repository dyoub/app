// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.ViewModel.Commercial.PurchaseSupplier;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchaseSupplier;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Inventory
{
    public class PurchaseSupplierController : TenantController
    {
        public PurchaseSupplierController() { }

        public PurchaseSupplierController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("purchase-orders/details/{id:int}/supplier"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/PurchaseSupplier/PurchaseSupplierDetails.cshtml");
        }

        [HttpGet, Route("purchase-orders/edit/{id:int}/supplier"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Inventory/PurchaseSupplier/PurchaseSupplierEdit.cshtml");
        }

        [HttpPost, Route("purchase-orders/supplier"), Authorization(Scope = "purchase-orders.read")]
        public async Task<ActionResult> Find(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .IncludeSupplier()
                .SingleOrDefaultAsync();

            return new PurchaseSupplierJson(purchaseOrder);
        }

        [HttpPost, Route("purchase-orders/supplier/update"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Update(PurchaseSupplierViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.PurchaseOrderId.Value)
                .SingleOrDefaultAsync();

            if (purchaseOrder == null)
            {
                return this.Error("Purchase order not found.");
            }

            if (purchaseOrder.Confirmed)
            {
                return this.Error("Purchase order already confirmed.");
            }

            if (viewModel.SupplierId != null)
            {
                if (!await Tenant.Suppliers.WhereId(viewModel.SupplierId.Value).AnyAsync())
                {
                    return this.Error("Supplier order not found.");
                }
            }

            purchaseOrder.SupplierId = viewModel.SupplierId;
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
