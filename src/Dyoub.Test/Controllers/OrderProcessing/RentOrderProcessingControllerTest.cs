// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.OrderProcessing;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.OrderProcessing.RentContractProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.OrderProcessing
{
    [TestClass]
    public class RentContractProcessingControllerTest
    {
        [TestMethod]
        public async Task ConfirmRentContract()
        {
            ConfirmRentContractContext context = new ConfirmRentContractContext();
            RentContractProcessingController controller = new RentContractProcessingController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            await controller.Confirm(viewModel);

            Assert.IsTrue(context.RentContractHasBeenConfirmed());
            Assert.IsTrue(context.BillingValuesHaveBeenCalculated());
            Assert.IsTrue(context.RentIncomesHaveBeenGenerated());
            Assert.IsTrue(context.StockMovementHasBeenRegistered());
        }

        [TestMethod]
        public async Task ConfirmRentContractWithoutStockEnough()
        {
            ConfirmWithoutStockEnoughContext context = new ConfirmWithoutStockEnoughContext();
            RentContractProcessingController controller = new RentContractProcessingController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Confirm(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task ConfirmConfirmedRentContract()
        {
            ConfirmConfirmedRentContractContext context = new ConfirmConfirmedRentContractContext();
            RentContractProcessingController controller = new RentContractProcessingController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Confirm(viewModel);

            Assert.IsTrue(context.RentContractWasNotConfirmedMoreThanOnce());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task RevertRentContract()
        {
            RevertRentContractContext context = new RevertRentContractContext();
            RentContractProcessingController controller = new RentContractProcessingController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            await controller.Revert(viewModel);

            Assert.IsTrue(context.RentContractHasBeenReverted());
            Assert.IsTrue(context.BillingValuesHaveBeenReset());
            Assert.IsTrue(context.RentIncomesHaveBeenRemoved());
            Assert.IsTrue(context.StockTransacionsHaveBeenRemoved());
        }

        [TestMethod]
        public async Task RevertConfirmedRentContract()
        {
            RevertNotConfirmedRentContractContext context = new RevertNotConfirmedRentContractContext();
            RentContractProcessingController controller = new RentContractProcessingController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Revert(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
