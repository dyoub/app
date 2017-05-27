// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentedProductsReturn;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.RentContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentedProductsReturnControllerTest
    {
        [TestMethod]
        public async Task UpdateReturnedProducts()
        {
            UpdateReturnedProductsContext context = new UpdateReturnedProductsContext();
            RentedProductsReturnController controller = new RentedProductsReturnController(context);

            UpdateReturnedProductsViewModel viewModel = new UpdateReturnedProductsViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.DateOfReturn = DateTime.UtcNow.Date;
            viewModel.TimeOfReturn = DateTime.UtcNow.TimeOfDay;
            viewModel.Products.Add(new ReturnedProductViewModel
            {
                Id = context.Product.Id,
                ReturnedQuantity = 1
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.ReturnCompleted());
            Assert.IsTrue(context.ReturnedQuantityHasBeenUpdatedTo(1));
        }

        [TestMethod]
        public async Task UpdateReturnOfNotConfirmedRentContract()
        {
            UpdateReturnOfNotConfirmedRentContractContext context = new UpdateReturnOfNotConfirmedRentContractContext();
            RentedProductsReturnController controller = new RentedProductsReturnController(context);

            UpdateReturnedProductsViewModel viewModel = new UpdateReturnedProductsViewModel();
            viewModel.RentContractId = context.RentContract.Id;
            viewModel.DateOfReturn = DateTime.UtcNow.Date;
            viewModel.TimeOfReturn = DateTime.UtcNow.TimeOfDay;
            viewModel.Products.Add(new ReturnedProductViewModel
            {
                Id = context.Product.Id,
                ReturnedQuantity = 1
            });

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(result is ModelErrorsJson);
            Assert.IsTrue(context.ReturnedQuantityNotChanged());

        }
    }
}
