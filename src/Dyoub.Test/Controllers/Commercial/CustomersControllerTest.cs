// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.Customers;
using Dyoub.App.Results.Commercial.Customers;
using Dyoub.Test.Contexts.Commercial.Customers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using Dyoub.App.Results.Common;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class CustomersControllerTest
    {
        [TestMethod]
        public async Task CreateCustomer()
        {
            CreateCustomerContext context = new CreateCustomerContext();
            CustomersController controller = new CustomersController(context);

            CreateCustomerViewModel viewModel = new CreateCustomerViewModel();
            viewModel.Name = "Customer Test";

            await controller.Create(viewModel);

            Assert.IsTrue(context.CustomerWasCreated());
        }

        [TestMethod]
        public async Task DeleteCustomer()
        {
            DeleteCustomerContext context = new DeleteCustomerContext();
            CustomersController controller = new CustomersController(context);

            CustomerIdViewModel viewModel = new CustomerIdViewModel();
            viewModel.Id = context.Customer.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.CustomerWasDeleted());
        }

        [TestMethod]
        public async Task DeleteCustomerWithSaleOrders()
        {
            DeleteCustomerWithSaleOrdersContext context = new DeleteCustomerWithSaleOrdersContext();
            CustomersController controller = new CustomersController(context);

            CustomerIdViewModel viewModel = new CustomerIdViewModel();
            viewModel.Id = context.Customer.Id;

            ActionResult result = await controller.Delete(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task FindCustomer()
        {
            FindCustomerContext context = new FindCustomerContext();
            CustomersController controller = new CustomersController(context);

            CustomerIdViewModel viewModel = new CustomerIdViewModel();
            viewModel.Id = context.Customer.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is CustomerJson);
        }

        [TestMethod]
        public async Task ListCustomer()
        {
            ListCustomerContext context = new ListCustomerContext();
            CustomersController controller = new CustomersController(context);

            CustomerListJson result = (CustomerListJson)await controller.List(new ListCustomersViewModel());

            Assert.IsTrue(result.Customers.Count() == context.Customers.Count());
        }

        [TestMethod]
        public async Task UpdateCustomer()
        {
            UpdateCustomerContext context = new UpdateCustomerContext();
            CustomersController controller = new CustomersController(context);

            UpdateCustomerViewModel viewModel = new UpdateCustomerViewModel();
            viewModel.Id = context.Customer.Id;
            viewModel.Name = context.Customer.Name + " Updated";
            viewModel.Email = context.Customer.Email + " Updated";
            viewModel.NationalId = context.Customer.NationalId + " Updated";
            viewModel.PhoneNumber = context.Customer.PhoneNumber + " Updated";
            viewModel.AlternativePhoneNumber = context.Customer.AlternativePhoneNumber + " Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.CustomerWasUpdated());
        }
    }
}
