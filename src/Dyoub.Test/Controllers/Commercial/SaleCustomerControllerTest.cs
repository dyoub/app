// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.SaleCustomer;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleCustomer;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.SaleCustomer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class SaleCustomerControllerTest
    {
        [TestMethod]
        public async Task FindSaleCustomer()
        {
            FindSaleCustomerContext context = new FindSaleCustomerContext();
            SaleCustomerController controller = new SaleCustomerController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is SaleCustomerJson);
        }
        
        [TestMethod]
        public async Task UpdateSaleCustomer()
        {
            UpdateSaleCustomerContext context = new UpdateSaleCustomerContext();
            SaleCustomerController controller = new SaleCustomerController(context);

            SaleCustomerViewModel viewModel = new SaleCustomerViewModel();
            viewModel.SaleOrderId = context.SaleOrder.Id;
            viewModel.Customer = new CustomerViewModel();
            viewModel.Customer.Id = context.AnotherCustomer.Id;
            viewModel.Customer.Name = context.AnotherCustomer.Name;

            await controller.Update(viewModel);

            Assert.IsTrue(context.SaleOrderWasUpdated());
        }

        [TestMethod]
        public async Task UpdateSaleCustomerToConfirmedSaleOrder()
        {
            UpdateCustomerToConfirmedSaleOrderContext context = new UpdateCustomerToConfirmedSaleOrderContext();
            SaleCustomerController controller = new SaleCustomerController(context);

            SaleCustomerViewModel viewModel = new SaleCustomerViewModel();
            viewModel.SaleOrderId = context.SaleOrder.Id;
            viewModel.Customer = new CustomerViewModel();
            viewModel.Customer.Id = context.AnotherCustomer.Id;
            viewModel.Customer.Name = context.AnotherCustomer.Name;

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.SaleOrderWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
