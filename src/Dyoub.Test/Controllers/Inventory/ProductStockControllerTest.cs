// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.ProductStock;
using Dyoub.App.Results.Inventory.ProductStock;
using Dyoub.Test.Contexts.Inventory.ProductStock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Inventory
{
    [TestClass]
    public class ProductStockControllerTest
    {
        [TestMethod]
        public async Task ListProductQuantities()
        {
            ListProductStockContext context = new ListProductStockContext();
            ProductStockController controller = new ProductStockController(context);

            ActionResult result = await controller.List(new ListProductStockViewModel());

            Assert.IsTrue(result is ProductStockListJson);
        }

        [TestMethod]
        public async Task ListPricingTableItems()
        {
            ListProductQuantitiesContext context = new ListProductQuantitiesContext();
            ProductStockController controller = new ProductStockController(context);

            ListProductQuantitiesViewModel viewModel = new ListProductQuantitiesViewModel();
            viewModel.StoreId = context.Store.Id;

            ActionResult result = await controller.ListProducts(viewModel);

            Assert.IsTrue(result is ProductQuantityListJson);
        }
    }
}
