// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Catalog;
using Dyoub.App.Models.ViewModel.Catalog.Services;
using Dyoub.App.Results.Catalog.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using Dyoub.Test.Contexts.Catalog.Services;

namespace Dyoub.Test.Controllers.Manage
{
    [TestClass]
    public class ServicesControllerTest
    {
        [TestMethod]
        public async Task CreateService()
        {
            CreateServiceContext context = new CreateServiceContext();
            ServicesController controller = new ServicesController(context);

            CreateServiceViewModel viewModel = new CreateServiceViewModel();
            viewModel.Name = "Service Test";
            viewModel.Marketed = true;

            await controller.Create(viewModel);

            Assert.IsTrue(context.ServiceWasCreated());
        }

        [TestMethod]
        public async Task DeleteService()
        {
            DeleteServiceContext context = new DeleteServiceContext();
            ServicesController controller = new ServicesController(context);

            ServiceIdViewModel viewModel = new ServiceIdViewModel();
            viewModel.Id = context.Service.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.ServiceWasDeleted());
        }

        [TestMethod]
        public async Task FindService()
        {
            FindServiceContext context = new FindServiceContext();
            ServicesController controller = new ServicesController(context);

            ServiceIdViewModel viewModel = new ServiceIdViewModel();
            viewModel.Id = context.Service.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is ServiceJson);
        }

        [TestMethod]
        public async Task ListService()
        {
            ListServiceContext context = new ListServiceContext();
            ServicesController controller = new ServicesController(context);

            ServiceListJson result = (ServiceListJson)await controller.List(new ListServicesViewModel());

            Assert.IsTrue(result.Services.Count() == context.Services.Count());
        }

        [TestMethod]
        public async Task UpdateService()
        {
            UpdateServiceContext context = new UpdateServiceContext();
            ServicesController controller = new ServicesController(context);

            UpdateServiceViewModel viewModel = new UpdateServiceViewModel();
            viewModel.Id = context.Service.Id;
            viewModel.Name = context.Service.Name + " Updated";
            viewModel.Code = context.Service.Code + " Updated";
            viewModel.AdditionalInformation = context.Service.AdditionalInformation + " Updated";
            viewModel.Marketed = !context.Service.Marketed;
            viewModel.CanFraction = !context.Service.CanFraction;

            await controller.Update(viewModel);

            Assert.IsTrue(context.ServiceWasUpdated());
        }
    }
}
