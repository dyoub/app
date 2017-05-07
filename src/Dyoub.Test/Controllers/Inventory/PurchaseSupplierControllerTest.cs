// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseSupplier;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchaseSupplier;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Inventory.PurchaseSupplier;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Inventory
{
    [TestClass]
    public class PurchaseSupplierControllerTest
    {
        [TestMethod]
        public async Task FindPurchaseSupplier()
        {
            FindPurchaseSupplierContext context = new FindPurchaseSupplierContext();
            PurchaseSupplierController controller = new PurchaseSupplierController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is PurchaseSupplierJson);
        }

        [TestMethod]
        public async Task UpdatePurchaseSupplier()
        {
            UpdatePurchaseSupplierContext context = new UpdatePurchaseSupplierContext();
            PurchaseSupplierController controller = new PurchaseSupplierController(context);

            PurchaseSupplierViewModel viewModel = new PurchaseSupplierViewModel();
            viewModel.PurchaseOrderId = context.PurchaseOrder.Id;
            viewModel.SupplierId = context.AnotherSupplier.Id;

            await controller.Update(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasUpdated());
        }

        [TestMethod]
        public async Task UpdatePurchaseSupplierToConfirmedPurchaseOrder()
        {
            UpdateSupplierToConfirmedPurchaseOrderContext context = new UpdateSupplierToConfirmedPurchaseOrderContext();
            PurchaseSupplierController controller = new PurchaseSupplierController(context);

            PurchaseSupplierViewModel viewModel = new PurchaseSupplierViewModel();
            viewModel.PurchaseOrderId = context.PurchaseOrder.Id;
            viewModel.SupplierId = context.AnotherSupplier.Id;

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
