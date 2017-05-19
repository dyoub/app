// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchaseOrders;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Inventory.PurchaseOrders;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Inventory
{
    [TestClass]
    public class PurchaseOrdersControllerTest
    {
        [TestMethod]
        public async Task CreatePurchaseOrder()
        {
            CreatePurchaseOrderContext context = new CreatePurchaseOrderContext();
            UserIdentity userIdentity = new UserIdentity { Email = "user@email.com" };

            PurchaseOrdersController controller = new PurchaseOrdersController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(userIdentity);

            CreatePurchaseOrderViewModel viewModel = new CreatePurchaseOrderViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.IssueDate = DateTime.UtcNow.Date;

            await controller.Create(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasCreated());
        }

        [TestMethod]
        public async Task DeletePurchaseOrder()
        {
            DeletePurchaseOrderContext context = new DeletePurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasDeleted());
        }

        [TestMethod]
        public async Task DeleteConfirmedPurchaseOrder()
        {
            DeleteConfirmedPurchaseOrderContext context = new DeleteConfirmedPurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            ActionResult result = await controller.Delete(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasNotDeleted());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task FindPurchaseOrder()
        {
            FindPurchaseOrderContext context = new FindPurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is PurchaseOrderJson);
        }

        [TestMethod]
        public async Task ListPurchaseOrder()
        {
            ListPurchaseOrderContext context = new ListPurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            PurchaseOrderListJson result = (PurchaseOrderListJson)await controller.List(new ListPurchaseOrdersViewModel());

            Assert.IsTrue(result.PurchaseOrders.Count() == context.PurchaseOrders.Count());
        }

        [TestMethod]
        public async Task UpdatePurchaseOrder()
        {
            UpdatePurchaseOrderContext context = new UpdatePurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            UpdatePurchaseOrderViewModel viewModel = new UpdatePurchaseOrderViewModel();
            viewModel.Id = context.PurchaseOrder.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.IssueDate = DateTime.UtcNow.Date.AddDays(1);
            viewModel.AdditionalInformation = context.PurchaseOrder.AdditionalInformation + "Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasUpdated());
        }

        [TestMethod]
        public async Task UpdateConfirmedPurchaseOrder()
        {
            UpdateConfirmedPurchaseOrderContext context = new UpdateConfirmedPurchaseOrderContext();
            PurchaseOrdersController controller = new PurchaseOrdersController(context);

            UpdatePurchaseOrderViewModel viewModel = new UpdatePurchaseOrderViewModel();
            viewModel.Id = context.PurchaseOrder.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.IssueDate = DateTime.UtcNow.Date.AddDays(1);
            viewModel.AdditionalInformation = context.PurchaseOrder.AdditionalInformation + "Updated";

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.PurchaseOrderWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
