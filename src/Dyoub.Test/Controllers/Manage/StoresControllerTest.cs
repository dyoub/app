// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Manage;
using Dyoub.App.Models.ViewModel.Manage.Stores;
using Dyoub.App.Results.Manage.Stores;
using Dyoub.Test.Contexts.Manage.Stores;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using Dyoub.App.Results.Common;

namespace Dyoub.Test.Controllers.Manage
{
    [TestClass]
    public class StoresControllerTest
    {
        [TestMethod]
        public async Task CreateStore()
        {
            CreateStoreContext context = new CreateStoreContext();
            StoresController controller = new StoresController(context);

            CreateStoreViewModel viewModel = new CreateStoreViewModel();
            viewModel.Name = "Store Test";
            viewModel.Active = true;

            await controller.Create(viewModel);

            Assert.IsTrue(context.StoreWasCreated());
        }

        [TestMethod]
        public async Task DeleteStore()
        {
            DeleteStoreContext context = new DeleteStoreContext();
            StoresController controller = new StoresController(context);

            StoreIdViewModel viewModel = new StoreIdViewModel();
            viewModel.Id = context.Store.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.StoreWasDeleted());
        }

        [TestMethod]
        public async Task DeleteStoreWithFixedExpenses()
        {
            DeleteStoreWithFixedExpensesContext context = new DeleteStoreWithFixedExpensesContext();
            StoresController controller = new StoresController(context);

            StoreIdViewModel viewModel = new StoreIdViewModel();
            viewModel.Id = context.Store.Id;

            ActionResult result = await controller.Delete(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public async Task FindStore()
        {
            FindStoreContext context = new FindStoreContext();
            StoresController controller = new StoresController(context);

            StoreIdViewModel viewModel = new StoreIdViewModel();
            viewModel.Id = context.Store.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is StoreJson);
        }

        [TestMethod]
        public async Task ListStore()
        {
            ListStoreContext context = new ListStoreContext();
            StoresController controller = new StoresController(context);

            StoreListJson result = (StoreListJson)await controller.List(new ListStoresViewModel());

            Assert.IsTrue(result.Stores.Count() == context.Stores.Count());
        }

        [TestMethod]
        public async Task UpdateStore()
        {
            UpdateStoreContext context = new UpdateStoreContext();
            StoresController controller = new StoresController(context);

            UpdateStoreViewModel viewModel = new UpdateStoreViewModel();
            viewModel.Id = context.Store.Id;
            viewModel.Name = context.Store.Name + " Updated";
            viewModel.Active = !context.Store.Active;

            await controller.Update(viewModel);

            Assert.IsTrue(context.StoreWasUpdated());
        }
    }
}
