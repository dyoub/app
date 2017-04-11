// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog;
using Dyoub.App.Models.ViewModel.Catalog.Products;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Catalog.Products;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Catalog
{
    public class ProductsController : TenantController
    {
        public ProductsController() { }

        public ProductsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("products/new"), Authorization(Scope = "products.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Catalog/Products/ProductEdit.cshtml");
        }

        [HttpGet, Route("products/details/{id:int}"), Authorization(Scope = "products.read")]
        public ActionResult Details()
        {
            return View("~/Views/Catalog/Products/ProductDetails.cshtml");
        }

        [HttpGet, Route("products/edit/{id:int}"), Authorization(Scope = "products.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Catalog/Products/ProductEdit.cshtml");
        }

        [HttpGet, Route("products"), Authorization(Scope = "products.read")]
        public ActionResult Index()
        {
            return View("~/Views/Catalog/Products/ProductList.cshtml");
        }

        [HttpPost, Route("products/create"), Authorization(Scope = "products.edit")]
        public async Task<ActionResult> Create(CreateProductViewModel viewModel)
        {
            Tenant.Products.Add(viewModel.MapTo(new Product()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("products/delete"), Authorization(Scope ="products.edit")]
        public async Task<ActionResult> Delete(ProductIdViewModel viewModel)
        {
            Product product = await Tenant.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return this.Error("Product not found.");
            }

            Tenant.Products.Remove(product);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("products/find"), Authorization(Scope = "products.read")]
        public async Task<ActionResult> Find(ProductIdViewModel viewModel)
        {
            Product product = await Tenant.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new ProductJson(product);
        }

        [HttpPost, Route("products"), Authorization(Scope = "products.read")]
        public async Task<ActionResult> List(ListProductsViewModel viewModel)
        {
            ICollection<Product> products = await Tenant.Products
                .OrderByName()
                .WhereNameContains(viewModel.Name.Words())
                .WhereCode(viewModel.Code)
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new ProductListJson(products);
        }

        [HttpPost, Route("products/update"), Authorization(Scope = "products.edit")]
        public async Task<ActionResult> Update(UpdateProductViewModel viewModel)
        {
            Product product = await Tenant.Products
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return this.Error("Product not found.");
            }

            viewModel.MapTo(product);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
