// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Common;
using Dyoub.App.Results.Inventory.PurchasedProducts;
using Dyoub.Test.Contexts.Inventory.PurchaseOrders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Inventory
{
    [TestClass]
    public class PurchasedProductsControllerTest
    {
        [TestMethod]
        public async Task ListPurchasedProducts()
        {
            ListPurchasedProductsContext context = new ListPurchasedProductsContext();
            PurchasedProductsController controller = new PurchasedProductsController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            PurchasedProductListJson result = (PurchasedProductListJson)await controller.List(viewModel);

            Assert.IsTrue(result.PurchaseOrder.PurchasedProducts.Count() == context.PurchasedProducts.Count());
        }

        [TestMethod]
        public async Task UpdatePurchasedProducts()
        {
            UpdatePurchasedProductsContext context = new UpdatePurchasedProductsContext();
            PurchasedProductsController controller = new PurchasedProductsController(context);

            UpdatePurchasedProductListViewModel viewModel = new UpdatePurchasedProductListViewModel();
            viewModel.PurchaseOrderId = context.PurchaseOrder.Id;
            viewModel.Products.Add(new PurchasedProductViewModel
            {
                Id = context.AnotherProduct.Id,
                UnitCost = 5.00M,
                Quantity = 1
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.PurchasedProductsWasUpdated());
        }

        [TestMethod]
        public async Task UpdateItemsOfConfirmedPurchaseOrder()
        {
            UpdateItemsOfConfirmedPurchaseOrderContext context = new UpdateItemsOfConfirmedPurchaseOrderContext();
            PurchasedProductsController controller = new PurchasedProductsController(context);

            UpdatePurchasedProductListViewModel viewModel = new UpdatePurchasedProductListViewModel();
            viewModel.PurchaseOrderId = context.PurchaseOrder.Id;
            viewModel.Products.Add(new PurchasedProductViewModel
            {
                Id = context.AnotherProduct.Id,
                UnitCost = 5.00M,
                Quantity = 1
            });

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.PurchasedProductsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
