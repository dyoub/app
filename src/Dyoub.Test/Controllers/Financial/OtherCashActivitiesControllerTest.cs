// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Financial;
using Dyoub.App.Models.ViewModel.Financial.OtherCashActivities;
using Dyoub.App.Results.Financial.OtherCashActivities;
using Dyoub.Test.Contexts.Financial.OtherCashActivities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Financial
{
    [TestClass]
    public class OtherCashActivitiesControllerTest
    {
        [TestMethod]
        public async Task CreateOtherCashActivity()
        {
            CreateOtherCashActivityContext context = new CreateOtherCashActivityContext();
            OtherCashActivitiesController controller = new OtherCashActivitiesController(context);

            CreateOtherCashActivityViewModel viewModel = new CreateOtherCashActivityViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.Description = "Test";
            viewModel.Date = DateTime.Now;
            viewModel.Value = 10.50M;

            await controller.Create(viewModel);

            Assert.IsTrue(context.OtherCashActivityWasCreated());
        }

        [TestMethod]
        public async Task DeleteOtherCashActivity()
        {
            DeleteOtherCashActivityContext context = new DeleteOtherCashActivityContext();
            OtherCashActivitiesController controller = new OtherCashActivitiesController(context);

            OtherCashActivityIdViewModel viewModel = new OtherCashActivityIdViewModel();
            viewModel.Id = context.OtherCashActivity.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.OtherCashActivityWasDeleted());
        }

        [TestMethod]
        public async Task FindOtherCashActivity()
        {
            FindOtherCashActivityContext context = new FindOtherCashActivityContext();
            OtherCashActivitiesController controller = new OtherCashActivitiesController(context);

            OtherCashActivityIdViewModel viewModel = new OtherCashActivityIdViewModel();
            viewModel.Id = context.OtherCashActivity.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is OtherCashActivityJson);
        }

        [TestMethod]
        public async Task ListOtherCashActivity()
        {
            ListOtherCashActivityContext context = new ListOtherCashActivityContext();
            OtherCashActivitiesController controller = new OtherCashActivitiesController(context);

            OtherCashActivityListJson result = (OtherCashActivityListJson)await controller.List(new ListOtherCashActivitiesViewModel());

            Assert.IsTrue(result.OtherCashActivities.Count() == context.OtherCashActivities.Count());
        }

        [TestMethod]
        public async Task UpdateOtherCashActivity()
        {
            UpdateOtherCashActivityContext context = new UpdateOtherCashActivityContext();
            OtherCashActivitiesController controller = new OtherCashActivitiesController(context);

            UpdateOtherCashActivityViewModel viewModel = new UpdateOtherCashActivityViewModel();
            viewModel.Id = context.OtherCashActivity.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.Description = context.OtherCashActivity.Description + " Updated";
            viewModel.Date = context.OtherCashActivity.Date.AddDays(1);
            viewModel.Value = context.OtherCashActivity.Value + 10;

            await controller.Update(viewModel);

            Assert.IsTrue(context.OtherCashActivityWasUpdated());
        }
    }
}
