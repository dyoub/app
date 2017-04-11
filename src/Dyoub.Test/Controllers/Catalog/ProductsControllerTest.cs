// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Catalog;
using Dyoub.App.Models.ViewModel.Catalog.Products;
using Dyoub.App.Results.Catalog.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;
using Dyoub.Test.Contexts.Catalog.Products;

namespace Dyoub.Test.Controllers.Manage
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public async Task CreateProduct()
        {
            CreateProductContext context = new CreateProductContext();
            ProductsController controller = new ProductsController(context);

            CreateProductViewModel viewModel = new CreateProductViewModel();
            viewModel.Name = "Product Test";
            viewModel.Marketed = true;

            await controller.Create(viewModel);

            Assert.IsTrue(context.ProductWasCreated());
        }

        [TestMethod]
        public async Task DeleteProduct()
        {
            DeleteProductContext context = new DeleteProductContext();
            ProductsController controller = new ProductsController(context);

            ProductIdViewModel viewModel = new ProductIdViewModel();
            viewModel.Id = context.Product.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.ProductWasDeleted());
        }

        [TestMethod]
        public async Task FindProduct()
        {
            FindProductContext context = new FindProductContext();
            ProductsController controller = new ProductsController(context);

            ProductIdViewModel viewModel = new ProductIdViewModel();
            viewModel.Id = context.Product.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is ProductJson);
        }

        [TestMethod]
        public async Task ListProduct()
        {
            ListProductContext context = new ListProductContext();
            ProductsController controller = new ProductsController(context);

            ProductListJson result = (ProductListJson)await controller.List(new ListProductsViewModel());

            Assert.IsTrue(result.Products.Count() == context.Products.Count());
        }

        [TestMethod]
        public async Task UpdateProduct()
        {
            UpdateProductContext context = new UpdateProductContext();
            ProductsController controller = new ProductsController(context);

            UpdateProductViewModel viewModel = new UpdateProductViewModel();
            viewModel.Id = context.Product.Id;
            viewModel.Name = context.Product.Name + " Updated";
            viewModel.Code = context.Product.Code + " Updated";
            viewModel.AdditionalInformation = context.Product.AdditionalInformation + " Updated";
            viewModel.Marketed = !context.Product.Marketed;
            viewModel.IsManufactured = !context.Product.IsManufactured;
            viewModel.StockMovement = !context.Product.StockMovement;
            viewModel.CanFraction = !context.Product.CanFraction;

            await controller.Update(viewModel);

            Assert.IsTrue(context.ProductWasUpdated());
        }
    }
}
