// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;
using Dyoub.App.Models.ServiceModel.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.SaleItems;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleItems;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class SaleItemsController : TenantController
    {
        public SaleItemsController() { }

        public SaleItemsController(TenantContext tenant) :base(tenant) { }

        [HttpGet, Route("sale-orders/details/{id:int}/items"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/SaleItems/SaleItemsDetails.cshtml");
        }

        [HttpGet, Route("sale-orders/edit/{id:int}/items"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/SaleItems/SaleItemsEdit.cshtml");
        }

        [HttpPost, Route("sale-orders/items"), Authorization(Scope = "sale-orders.read")]
        public async Task<ActionResult> List(SaleOrderIdViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            ICollection<SaleItem> itemList = null;

            if (saleOrder != null)
            {
                IQueryable<SaleItem> productItems = Tenant.SaleProducts
                    .WhereSaleOrderId(saleOrder.Id)
                    .AsSaleItem();

                IQueryable<SaleItem> serviceItems = Tenant.SaleServices
                    .WhereSaleOrderId(saleOrder.Id)
                    .AsSaleItem();

                itemList = await productItems.Concat(serviceItems).ToListAsync();
            }

            return new SaleItemListJson(saleOrder, itemList);
        }

        [HttpPost, Route("sale-orders/items/update"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Update(UpdateSaleItemListViewModel viewModel)
        {
            SaleShoppingCart shoppingCart = new SaleShoppingCart(Tenant);

            if (!await shoppingCart.Checkout(viewModel.SaleOrderId.Value, viewModel.Map()))
            {
                if (shoppingCart.SaleOrder == null)
                {
                    return this.Error("Sale order not found.");
                }

                if (shoppingCart.SaleOrder.Confirmed)
                {
                    return this.Error("Sale order already confirmed.");
                }

                if (shoppingCart.HasOneOrMoreItemsNotFound)
                {
                    return this.Error("One or more items not found.");
                }

                if (shoppingCart.HasOneOrMorePricesNotDefined)
                {
                    return this.Error("One or more prices not defined.");
                }

                if (shoppingCart.HasItemWithTotalNegative)
                {
                    return this.Error("One or more items have total negative.");
                }
            }
            
            return this.Success();
        }
    }
}
