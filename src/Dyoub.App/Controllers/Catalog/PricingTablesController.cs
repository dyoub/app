// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Catalog.PricingTables;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Models.ServiceModel.Catalog;
using Dyoub.App.Results.Catalog.PricingTables;
using System.Collections.Generic;
using System.Linq;
using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;

namespace Dyoub.App.Controllers.Catalog
{
    public class PricingTablesController : TenantController
    {
        public PricingTablesController() { }

        public PricingTablesController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("pricing-tables/copy-to/{storeId:int}"), Authorization(Scope = "pricing-tables.read")]
        public ActionResult CopyTo()
        {
            return View("~/Views/Catalog/PricingTables/CopyToPricingTable.cshtml");
        }

        [HttpGet, Route("pricing-tables/details/{storeId:int}"), Authorization(Scope = "pricing-tables.read")]
        public ActionResult Details()
        {
            return View("~/Views/Catalog/PricingTables/PricingTableDetails.cshtml");
        }

        [HttpGet, Route("pricing-tables/edit/{storeId:int}"), Authorization(Scope = "pricing-tables.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Catalog/PricingTables/PricingTableEdit.cshtml");
        }

        [HttpGet, Route("pricing-tables"), Authorization(Scope = "pricing-tables.read")]
        public ActionResult Index()
        {
            return View("~/Views/Catalog/PricingTables/PricingTableList.cshtml");
        }

        [HttpPost, Route("pricing-tables/clear"), Authorization(Scope = "pricing-tables.edit")]
        public async Task<ActionResult> Clear(PricingTableIdViewModel viewModel)
        {
            PricingTable pricingTable = new PricingTable(Tenant, viewModel.StoreId.Value);
            
            if (!await pricingTable.Clear())
            {
                return this.Error("Pricing table not found.");
            }
            
            return this.Success();
        }

        [HttpPost, Route("pricing-tables/copy"), Authorization(Scope = "pricing-tables.read")]
        public async Task<ActionResult> Copy(CopyPricesViewModel viewModel)
        {
            PricingTable pricingTable = new PricingTable(Tenant, viewModel.StoreId.Value);

            if (!await pricingTable.CopyFrom(viewModel.SourceStoreId.Value))
            {
                return this.Error("Pricing table not found.");
            }
            
            return this.Success();
        }

        [HttpPost, Route("pricing-tables/find"), Authorization(Scope = "pricing-tables.read")]
        public async Task<ActionResult> Find(PricingTableIdViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.StoreId.Value)
                .SingleOrDefaultAsync();

            return new PricingTableJson(store);
        }

        [HttpPost, Route("pricing-tables"), Authorization(Scope = "pricing-tables.read")]
        public async Task<ActionResult> List(ListPricingTablesViewModel viewModel)
        {
            ICollection<Store> stores = await Tenant.Stores
                .WhereNameContains(viewModel.StoreName.Words())
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new PricingTableListJson(stores);
        }

        [HttpPost, Route("pricing-tables/items"), Authorization(Scope = "ItemPrices:Read")]
        public async Task<ActionResult> ListItems(ListItemsViewModel viewModel)
        {
            IQueryable<ItemPrice> productItems = Tenant.Products.AsItemPrice(viewModel.StoreId.Value);
            IQueryable<ItemPrice> serviceItems = Tenant.Services.AsItemPrice(viewModel.StoreId.Value);
            IQueryable<ItemPrice> fetchedItems = null;

            switch (viewModel.SearchFor)
            {
                case ListItemsViewModel.Filter.All: fetchedItems = productItems.Concat(serviceItems); break;
                case ListItemsViewModel.Filter.Products: fetchedItems = productItems; break;
                case ListItemsViewModel.Filter.Services: fetchedItems = serviceItems; break;
            }

            ICollection<ItemPrice> itemPrices = await fetchedItems
                .WherwCode(viewModel.Code)
                .WhereNameContains(viewModel.Name.Words())
                .OrderedByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new ItemPriceListJson(itemPrices);
        }

        [HttpPost, Route("pricing-tables/items/for-sale"), Authorization(Scope = "item-prices.read")]
        public async Task<ActionResult> ListItemsForSale(ListItemsForSaleViewModel viewModel)
        {
            IQueryable<ItemPrice> productItems = Tenant.ProductPrices
                .WhereStoreId(viewModel.StoreId.Value)
                .AsItemPrice();

            IQueryable<ItemPrice> serviceItems = Tenant.ServicePrices
                .WhereStoreId(viewModel.StoreId.Value)
                .AsItemPrice();

            ICollection<ItemPrice> itemPrices = await productItems.Concat(serviceItems)
                .WhereCodeOrName(viewModel.NameOrCode.Words())
                .OrderedByName()
                .Take(3)
                .ToListAsync();

            return new ItemPriceListJson(itemPrices);
        }

        [HttpPost, Route("pricing-tables/update"), Authorization(Scope = "pricing-tables.edit")]
        public async Task<ActionResult> Update(UpdatePricesViewModel viewModel)
        {
            PricingTable pricingTable = new PricingTable(Tenant, viewModel.StoreId.Value);

            if (!await pricingTable.Update(viewModel.MapToItemPrices()))
            {
                return this.Error("Pricing table not found.");
            }

            return this.Success();
        }
    }
}
