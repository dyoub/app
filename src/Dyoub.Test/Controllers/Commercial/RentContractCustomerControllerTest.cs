// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentContractCustomer;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContractCustomer;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.RentContractCustomer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentContractCustomerControllerTest
    {
        [TestMethod]
        public async Task FindRentContractCustomer()
        {
            FindRentContractCustomerContext context = new FindRentContractCustomerContext();
            RentContractCustomerController controller = new RentContractCustomerController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is RentContractCustomerJson);
        }

        [TestMethod]
        public async Task UpdateRentContractCustomer()
        {
            UpdateRentContractCustomerContext context = new UpdateRentContractCustomerContext();
            RentContractCustomerController controller = new RentContractCustomerController(context);

            RentContractCustomerViewModel viewModel = new RentContractCustomerViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.CustomerId = context.AnotherCustomer.Id;

            await controller.Update(viewModel);

            Assert.IsTrue(context.RentContractWasUpdated());
        }

        [TestMethod]
        public async Task UpdateRentContractCustomerToConfirmedRentContract()
        {
            UpdateCustomerToConfirmedRentContractContext context = new UpdateCustomerToConfirmedRentContractContext();
            RentContractCustomerController controller = new RentContractCustomerController(context);

            RentContractCustomerViewModel viewModel = new RentContractCustomerViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.CustomerId = context.AnotherCustomer.Id;

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.RentContractWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
