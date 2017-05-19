// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentedProducts;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Common;
using Dyoub.App.Results.Commercial.RentedProducts;
using Dyoub.Test.Contexts.Commercial.RentContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentedProductsControllerTest
    {
        [TestMethod]
        public async Task ListRentedProducts()
        {
            ListRentedProductsContext context = new ListRentedProductsContext();
            RentedProductsController controller = new RentedProductsController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            RentedProductListJson result = (RentedProductListJson)await controller.List(viewModel);

            Assert.IsTrue(result.RentedProducts.Count() == context.RentedProducts.Count());
        }

        [TestMethod]
        public async Task UpdateRentedProducts()
        {
            UpdateRentedProductsContext context = new UpdateRentedProductsContext();
            RentedProductsController controller = new RentedProductsController(context);

            UpdateRentedProductListViewModel viewModel = new UpdateRentedProductListViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.Products.Add(new RentedProductViewModel
            {
                Id = context.AnotherProduct.Id,
                Quantity = 1
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.RentedProductsWasUpdated());
        }

        [TestMethod]
        public async Task UpdateItemsOfConfirmedRentContract()
        {
            UpdateItemsOfConfirmedRentContractContext context = new UpdateItemsOfConfirmedRentContractContext();
            RentedProductsController controller = new RentedProductsController(context);

            UpdateRentedProductListViewModel viewModel = new UpdateRentedProductListViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.Products.Add(new RentedProductViewModel
            {
                Id = context.AnotherProduct.Id,
                Quantity = 1
            });

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.RentedProductsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
