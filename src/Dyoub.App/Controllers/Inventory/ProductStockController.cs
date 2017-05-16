// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.ProductQuantities;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Inventory.ProductStock;
using Dyoub.App.Results.Inventory.ProductStock;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Inventory
{
    public class ProductStockController : TenantController
    {
        public ProductStockController() { }

        public ProductStockController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("product-stock/details/{storeId:int}"), Authorization(Scope = "product-stock.read")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/ProductStock/ProductStockDetails.cshtml");
        }

        [HttpGet, Route("product-stock"), Authorization(Scope = "product-stock.read")]
        public ActionResult Index()
        {
            return View("~/Views/Inventory/ProductStock/ProductStockList.cshtml");
        }

        [HttpPost, Route("product-stock/find"), Authorization(Scope = "product-stock.read")]
        public async Task<ActionResult> Find(ProductStockIdViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.StoreId.Value)
                .SingleOrDefaultAsync();

            return new ProductStockJson(store);
        }

        [HttpPost, Route("product-stock"), Authorization(Scope = "product-stock.read")]
        public async Task<ActionResult> List(ListProductStockViewModel viewModel)
        {
            ICollection<Store> stores = await Tenant.Stores
                .WhereNameContains(viewModel.StoreName.Words())
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new ProductStockListJson(stores);
        }

        [HttpPost, Route("product-stock/quantities"), Authorization(Scope = "product-stock.read")]
        public async Task<ActionResult> ListProducts(ListProductQuantitiesViewModel viewModel)
        {
            ICollection<ProductQuantity> productQuantities = await Tenant.Products
                .WhereNameOrCode(viewModel.NameOrCode.Words())
                .AsProductQuantity(viewModel.StoreId.Value)
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new ProductQuantityListJson(productQuantities);
        }
    }
}
