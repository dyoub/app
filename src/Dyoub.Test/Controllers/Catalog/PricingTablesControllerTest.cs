// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Catalog;
using Dyoub.App.Models.ViewModel.Catalog.PricingTables;
using Dyoub.App.Results.Catalog.PricingTables;
using Dyoub.Test.Contexts.Catalog.PricingTables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Catalog
{
    [TestClass]
    public class PricingTablesControllerTest
    {
        [TestMethod]
        public async Task ClearPricingTable()
        {
            ClearPricingTableContext context = new ClearPricingTableContext();
            PricingTablesController controller = new PricingTablesController(context);

            PricingTableIdViewModel viewModel = new PricingTableIdViewModel();
            viewModel.StoreId = context.Store.Id;

            await controller.Clear(viewModel);

            Assert.IsTrue(context.PricingTableWasErased());
        }

        [TestMethod]
        public async Task CopyPricingTable()
        {
            CopyPricingTableContext context = new CopyPricingTableContext();
            PricingTablesController controller = new PricingTablesController(context);

            CopyPricesViewModel viewModel = new CopyPricesViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.SourceStoreId = context.SourceStore.Id;

            await controller.Copy(viewModel);

            Assert.IsTrue(context.PricingTableWasCopied());
        }

        [TestMethod]
        public async Task FindPricingTable()
        {
            FindPricingTableContext context = new FindPricingTableContext();
            PricingTablesController controller = new PricingTablesController(context);

            PricingTableIdViewModel viewModel = new PricingTableIdViewModel();
            viewModel.StoreId = context.Store.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is PricingTableJson);
        }

        [TestMethod]
        public async Task ListPricingTables()
        {
            ListPricingTablesContext context = new ListPricingTablesContext();
            PricingTablesController controller = new PricingTablesController(context);
            
            ActionResult result = await controller.List(new ListPricingTablesViewModel());

            Assert.IsTrue(result is PricingTableListJson);
        }

        [TestMethod]
        public async Task ListPricingTableItems()
        {
            ListPricingTableItemsContext context = new ListPricingTableItemsContext();
            PricingTablesController controller = new PricingTablesController(context);

            ListItemsViewModel viewModel = new ListItemsViewModel();
            viewModel.StoreId = context.Store.Id;

            ActionResult result = await controller.ListItems(viewModel);

            Assert.IsTrue(result is ItemPriceListJson);
        }

        [TestMethod]
        public async Task ListPricingTableItemsForSale()
        {
            ListPricingTableItemsForSaleContext context = new ListPricingTableItemsForSaleContext();
            PricingTablesController controller = new PricingTablesController(context);

            ListItemsForSaleViewModel viewModel = new ListItemsForSaleViewModel();
            viewModel.StoreId = context.Store.Id;

            ActionResult result = await controller.ListItemsForSale(viewModel);

            Assert.IsTrue(result is ItemPriceListJson);
        }

        [TestMethod]
        public async Task UpdatePricingTable()
        {
            UpdatePricingTableContext context = new UpdatePricingTableContext();
            PricingTablesController controller = new PricingTablesController(context);

            UpdatePricesViewModel viewModel = new UpdatePricesViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Shirt.Id, IsProduct =true, UnitPrice = 50.00M });
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Sneakers.Id, IsProduct = true, UnitPrice = null });
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Pants.Id, IsProduct = true, UnitPrice = 60.00M });
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Seam.Id, IsService = true, UnitPrice = 50.00M });
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Clean.Id, IsService = true, UnitPrice = null });
            viewModel.Items.Add(new ItemPriceViewModel { Id = context.Adjustment.Id, IsService = true, UnitPrice = 60.00M });

            await controller.Update(viewModel);

            Assert.IsTrue(context.NewPricesWasCreated());
            Assert.IsTrue(context.ChangedPricesWasUpdated());
            Assert.IsTrue(context.EmptyPricesWasDeleted());
        }
    }
}
