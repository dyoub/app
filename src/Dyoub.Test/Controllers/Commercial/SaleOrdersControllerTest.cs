// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleOrders;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.SaleOrders;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class SaleOrdersControllerTest
    {
        [TestMethod]
        public async Task CreateSaleOrder()
        {
            CreateSaleOrderContext context = new CreateSaleOrderContext();
            UserIdentity userIdentity = new UserIdentity { Email = "user@email.com" };

            SaleOrdersController controller = new SaleOrdersController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(userIdentity);

            CreateSaleOrderViewModel viewModel = new CreateSaleOrderViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.IssueDate = DateTime.Today;

            await controller.Create(viewModel);

            Assert.IsTrue(context.SaleOrderWasCreated());
        }

        [TestMethod]
        public async Task DeleteSaleOrder()
        {
            DeleteSaleOrderContext context = new DeleteSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.SaleOrderWasDeleted());
        }

        [TestMethod]
        public async Task DeleteConfirmedSaleOrder()
        {
            DeleteConfirmedSaleOrderContext context = new DeleteConfirmedSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            ActionResult result = await controller.Delete(viewModel);

            Assert.IsTrue(context.SaleOrderWasNotDeleted());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task FindSaleOrder()
        {
            FindSaleOrderContext context = new FindSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is SaleOrderJson);
        }

        [TestMethod]
        public async Task ListSaleOrder()
        {
            ListSaleOrderContext context = new ListSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            SaleOrderListJson result = (SaleOrderListJson)await controller.List(new ListSaleOrdersViewModel());

            Assert.IsTrue(result.SaleOrders.Count() == context.SaleOrders.Count());
        }

        [TestMethod]
        public async Task UpdateSaleOrder()
        {
            UpdateSaleOrderContext context = new UpdateSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            UpdateSaleOrderViewModel viewModel = new UpdateSaleOrderViewModel();
            viewModel.Id = context.SaleOrder.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.IssueDate = DateTime.Today.AddDays(1);
            viewModel.AdditionalInformation = context.SaleOrder.AdditionalInformation + "Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.SaleOrderWasUpdated());
        }

        [TestMethod]
        public async Task UpdateConfirmedSaleOrder()
        {
            UpdateConfirmedSaleOrderContext context = new UpdateConfirmedSaleOrderContext();
            SaleOrdersController controller = new SaleOrdersController(context);

            UpdateSaleOrderViewModel viewModel = new UpdateSaleOrderViewModel();
            viewModel.Id = context.SaleOrder.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.IssueDate = DateTime.Today.AddDays(1);
            viewModel.AdditionalInformation = context.SaleOrder.AdditionalInformation + "Updated";

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.SaleOrderWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
