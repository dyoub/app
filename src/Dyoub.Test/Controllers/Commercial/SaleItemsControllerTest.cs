// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Extensions;
using Dyoub.App.Infrastructure.Security;
using Dyoub.App.Models.ViewModel.Commercial.SaleItems;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleItems;
using Dyoub.App.Results.Commercial.SaleOrders;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.SaleOrders;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class SaleItemsControllerTest
    {
        [TestMethod]
        public async Task ListSaleItems()
        {
            ListSaleItemsContext context = new ListSaleItemsContext();
            SaleItemsController controller = new SaleItemsController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            SaleItemListJson result = (SaleItemListJson)await controller.List(viewModel);

            Assert.IsTrue(result.Items.Count() == context.Items().Count());
        }

        [TestMethod]
        public async Task UpdateSaleItems()
        {
            UpdateSaleItemsContext context = new UpdateSaleItemsContext();
            SaleItemsController controller = new SaleItemsController(context);

            UpdateSaleItemListViewModel viewModel = new UpdateSaleItemListViewModel();
            viewModel.SaleOrderId = context.SaleOrder.Id;
            viewModel.Items.Add(new SaleItemViewModel
            {
                Id = context.AnotherProduct.Id,
                IsProduct = true,
                Quantity = 1
            });
            viewModel.Items.Add(new SaleItemViewModel
            {
                Id = context.AnotherService.Id,
                IsService = true,
                Quantity = 1
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.SaleItemsWasUpdated());
        }

        [TestMethod]
        public async Task UpdateItemsOfConfirmedSaleOrder()
        {
            UpdateItemsOfConfirmedSaleOrderContext context = new UpdateItemsOfConfirmedSaleOrderContext();
            SaleItemsController controller = new SaleItemsController(context);

            UpdateSaleItemListViewModel viewModel = new UpdateSaleItemListViewModel();
            viewModel.SaleOrderId = context.SaleOrder.Id;
            viewModel.Items.Add(new SaleItemViewModel
            {
                Id = context.AnotherProduct.Id,
                IsProduct = true,
                Quantity = 1
            });
            viewModel.Items.Add(new SaleItemViewModel
            {
                Id = context.AnotherService.Id,
                IsService = true,
                Quantity = 1
            });

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.SaleItemsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);

        }
    }
}
