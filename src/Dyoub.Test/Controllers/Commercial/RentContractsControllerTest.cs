// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContracts;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.RentContracts;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentContractsControllerTest
    {
        [TestMethod]
        public async Task CreateRentContract()
        {
            CreateRentContractContext context = new CreateRentContractContext();
            UserIdentity userIdentity = new UserIdentity { Email = "user@email.com" };

            RentContractsController controller = new RentContractsController(context);
            controller.ControllerContext = new ControllerContextFake(controller);
            controller.HttpContext.SetUserIdentity(userIdentity);

            CreateRentContractViewModel viewModel = new CreateRentContractViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.StartDate = DateTime.UtcNow.Date;
            viewModel.StartTime = DateTime.UtcNow.TimeOfDay;
            viewModel.EndDate = DateTime.UtcNow.Date.AddDays(1);
            viewModel.EndTime = DateTime.UtcNow.TimeOfDay;
            viewModel.Location = "Brazil";

            await controller.Create(viewModel);

            Assert.IsTrue(context.RentContractWasCreated());
        }

        [TestMethod]
        public async Task DeleteRentContract()
        {
            DeleteRentContractContext context = new DeleteRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.RentContractWasDeleted());
        }

        [TestMethod]
        public async Task DeleteConfirmedRentContract()
        {
            DeleteConfirmedRentContractContext context = new DeleteConfirmedRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Delete(viewModel);

            Assert.IsTrue(context.RentContractWasNotDeleted());
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task FindRentContract()
        {
            FindRentContractContext context = new FindRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is RentContractJson);
        }

        [TestMethod]
        public async Task ListRentContract()
        {
            ListRentContractContext context = new ListRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            RentContractListJson result = (RentContractListJson)await controller.List(new ListRentContractsViewModel());

            Assert.IsTrue(result.RentContracts.Count() == context.RentContracts.Count());
        }

        [TestMethod]
        public async Task UpdateRentContract()
        {
            UpdateRentContractContext context = new UpdateRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            UpdateRentContractViewModel viewModel = new UpdateRentContractViewModel();
            viewModel.Id = context.RentContract.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.StartDate = DateTime.UtcNow.Date;
            viewModel.StartTime = DateTime.UtcNow.TimeOfDay;
            viewModel.EndDate = DateTime.UtcNow.Date.AddDays(1);
            viewModel.EndTime = DateTime.UtcNow.TimeOfDay;
            viewModel.Location = context.RentContract.Location + "Updated";
            viewModel.AdditionalInformation = context.RentContract.AdditionalInformation + "Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.RentContractWasUpdated());
        }

        [TestMethod]
        public async Task UpdateConfirmedRentContract()
        {
            UpdateConfirmedRentContractContext context = new UpdateConfirmedRentContractContext();
            RentContractsController controller = new RentContractsController(context);

            UpdateRentContractViewModel viewModel = new UpdateRentContractViewModel();
            viewModel.Id = context.RentContract.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.StartDate = context.RentContract.StartDate.AddDays(1);
            viewModel.EndDate = context.RentContract.EndDate.AddDays(1);
            viewModel.Location = context.RentContract.Location + "Updated";
            viewModel.AdditionalInformation = context.RentContract.AdditionalInformation + "Updated";

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.RentContractWasNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
