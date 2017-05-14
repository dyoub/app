// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.OrderProcessing;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.OrderProcessing.PurchaseOrderProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.OrderProcessing
{
    [TestClass]
    public class PurchaseOrderProcessingControllerTest
    {
        [TestMethod]
        public async Task ConfirmPurchaseOrder()
        {
            ConfirmPurchaseOrderContext context = new ConfirmPurchaseOrderContext();
            PurchaseOrderProcessingController controller = new PurchaseOrderProcessingController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            await controller.Confirm(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasConfirmed());
        }

        [TestMethod]
        public async Task ConfirmConfirmedPurchaseOrder()
        {
            ConfirmConfirmedPurchaseOrderContext context = new ConfirmConfirmedPurchaseOrderContext();
            PurchaseOrderProcessingController controller = new PurchaseOrderProcessingController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            ActionResult result = await controller.Confirm(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasNotConfirmedMoreThanOnce());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task RevertPurchaseOrder()
        {
            RevertPurchaseOrderContext context = new RevertPurchaseOrderContext();
            PurchaseOrderProcessingController controller = new PurchaseOrderProcessingController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            await controller.Revert(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasReverted());
        }

        [TestMethod]
        public async Task RevertConfirmedPurchaseOrder()
        {
            RevertNotConfirmedPurchaseOrderContext context = new RevertNotConfirmedPurchaseOrderContext();
            PurchaseOrderProcessingController controller = new PurchaseOrderProcessingController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            ActionResult result = await controller.Revert(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
