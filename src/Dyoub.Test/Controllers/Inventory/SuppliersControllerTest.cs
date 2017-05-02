// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.Suppliers;
using Dyoub.App.Results.Inventory.Suppliers;
using Dyoub.Test.Contexts.Inventory.Suppliers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class SuppliersControllerTest
    {
        [TestMethod]
        public async Task CreateSupplier()
        {
            CreateSupplierContext context = new CreateSupplierContext();
            SuppliersController controller = new SuppliersController(context);

            CreateSupplierViewModel viewModel = new CreateSupplierViewModel();
            viewModel.Name = "Supplier Test";

            await controller.Create(viewModel);

            Assert.IsTrue(context.SupplierWasCreated());
        }

        [TestMethod]
        public async Task DeleteSupplier()
        {
            DeleteSupplierContext context = new DeleteSupplierContext();
            SuppliersController controller = new SuppliersController(context);

            SupplierIdViewModel viewModel = new SupplierIdViewModel();
            viewModel.Id = context.Supplier.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.SupplierWasDeleted());
        }
        
        [TestMethod]
        public async Task FindSupplier()
        {
            FindSupplierContext context = new FindSupplierContext();
            SuppliersController controller = new SuppliersController(context);

            SupplierIdViewModel viewModel = new SupplierIdViewModel();
            viewModel.Id = context.Supplier.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is SupplierJson);
        }

        [TestMethod]
        public async Task ListSupplier()
        {
            ListSupplierContext context = new ListSupplierContext();
            SuppliersController controller = new SuppliersController(context);

            SupplierListJson result = (SupplierListJson)await controller.List(new ListSuppliersViewModel());

            Assert.IsTrue(result.Suppliers.Count() == context.Suppliers.Count());
        }

        [TestMethod]
        public async Task UpdateSupplier()
        {
            UpdateSupplierContext context = new UpdateSupplierContext();
            SuppliersController controller = new SuppliersController(context);

            UpdateSupplierViewModel viewModel = new UpdateSupplierViewModel();
            viewModel.Id = context.Supplier.Id;
            viewModel.Name = context.Supplier.Name + " Updated";
            viewModel.NationalId = context.Supplier.NationalId + " Updated";
            viewModel.Email = context.Supplier.Email + " Updated";
            viewModel.Homepage = context.Supplier.Homepage + " Updated";
            viewModel.Facebook = context.Supplier.Facebook + " Updated";
            viewModel.Instagram = context.Supplier.Instagram + " Updated";
            viewModel.Twitter = context.Supplier.Twitter + " Updated";
            viewModel.GooglePlus = context.Supplier.GooglePlus + " Updated";
            viewModel.PhoneNumber = context.Supplier.PhoneNumber + " Updated";
            viewModel.AlternativePhoneNumber = context.Supplier.AlternativePhoneNumber + " Updated";

            await controller.Update(viewModel);

            Assert.IsTrue(context.SupplierWasUpdated());
        }
    }
}
