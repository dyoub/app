// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.OrderProcessing;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.OrderProcessing.SaleOrderProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.OrderProcessing
{
    [TestClass]
    public class SaleOrderProcessingControllerTest
    {
        [TestMethod]
        public async Task ConfirmSaleOrder()
        {
            ConfirmSaleOrderContext context = new ConfirmSaleOrderContext();
            SaleOrderProcessingController controller = new SaleOrderProcessingController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            await controller.Confirm(viewModel);

            Assert.IsTrue(context.SaleOrderWasConfirmed());
        }

        [TestMethod]
        public async Task ConfirmConfirmedSaleOrder()
        {
            ConfirmConfirmedSaleOrderContext context = new ConfirmConfirmedSaleOrderContext();
            SaleOrderProcessingController controller = new SaleOrderProcessingController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            ActionResult result = await controller.Confirm(viewModel);

            Assert.IsTrue(context.SaleOrderWasNotConfirmedMoreThanOnce());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task RevertSaleOrder()
        {
            RevertSaleOrderContext context = new RevertSaleOrderContext();
            SaleOrderProcessingController controller = new SaleOrderProcessingController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            await controller.Revert(viewModel);

            Assert.IsTrue(context.SaleOrderWasReverted());
        }

        [TestMethod]
        public async Task RevertConfirmedSaleOrder()
        {
            RevertNotConfirmedSaleOrderContext context = new RevertNotConfirmedSaleOrderContext();
            SaleOrderProcessingController controller = new SaleOrderProcessingController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            ActionResult result = await controller.Revert(viewModel);
            
            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
