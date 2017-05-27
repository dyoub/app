// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.ServiceModel.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchasedProducts;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Inventory
{
    public class PurchasedProductsController : TenantController
    {
        public PurchasedProductsController() { }

        public PurchasedProductsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("purchase-orders/details/{id:int}/products"), Authorization(Scope = "purchase-orders.read")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/PurchasedProducts/PurchasedProductsDetails.cshtml");
        }

        [HttpGet, Route("purchase-orders/edit/{id:int}/products"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Inventory/PurchasedProducts/PurchasedProductsEdit.cshtml");
        }

        [HttpPost, Route("purchase-orders/products"), Authorization(Scope = "purchase-orders.read")]
        public async Task<ActionResult> List(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .IncludePurchasedProducts()
                .SingleOrDefaultAsync();
            
            return new PurchasedProductListJson(purchaseOrder);
        }

        [HttpPost, Route("purchase-orders/products/update"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Update(UpdatePurchasedProductListViewModel viewModel)
        {
            PurchaseShoppingCart shoppingCart = new PurchaseShoppingCart(Tenant);

            if (!await shoppingCart.Checkout(viewModel.PurchaseOrderId.Value, viewModel.Map()))
            {
                if (shoppingCart.PurchaseOrder == null)
                {
                    return this.Error("Purchase order not found.");
                }

                if (shoppingCart.PurchaseOrder.Confirmed)
                {
                    return this.Error("Purchase order already confirmed.");
                }

                if (shoppingCart.HasOneOrMoreProductsNotFound)
                {
                    return this.Error("One or more products not found.");
                }

                if (shoppingCart.HasProductWithTotalNegative)
                {
                    return this.Error("One or more products have total negative.");
                }
            }

            return this.Success();
        }
    }
}
