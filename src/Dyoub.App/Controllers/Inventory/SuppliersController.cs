// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.ViewModel.Inventory.Suppliers;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Inventory.Suppliers;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Inventory
{
    public class SuppliersController : TenantController
    {
        public SuppliersController() { }

        public SuppliersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("suppliers/new"), Authorization(Scope = "suppliers.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Inventory/Suppliers/SupplierEdit.cshtml");
        }

        [HttpGet, Route("suppliers/details/{id:int}"), Authorization(Scope = "suppliers.read")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/Suppliers/SupplierDetails.cshtml");
        }

        [HttpGet, Route("suppliers/edit/{id:int}"), Authorization(Scope = "suppliers.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Inventory/Suppliers/SupplierEdit.cshtml");
        }

        [HttpGet, Route("suppliers"), Authorization(Scope = "suppliers.read")]
        public ActionResult Index()
        {
            return View("~/Views/Inventory/Suppliers/SupplierList.cshtml");
        }

        [HttpPost, Route("suppliers/create"), Authorization(Scope = "suppliers.edit")]
        public async Task<ActionResult> Create(CreateSupplierViewModel viewModel)
        {
            Supplier supplier = Tenant.Suppliers.Add(viewModel.MapTo(new Supplier()));
            await Tenant.SaveChangesAsync();

            return new SupplierJson(supplier);
        }

        [HttpPost, Route("suppliers/delete"), Authorization(Scope = "suppliers.edit")]
        public async Task<ActionResult> Delete(SupplierIdViewModel viewModel)
        {
            Supplier supplier = await Tenant.Set<Supplier>()
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (supplier == null)
            {
                return this.Error("Supplier not found.");
            }
            
            Tenant.Suppliers.Remove(supplier);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("suppliers/find"), Authorization(Scope = "suppliers.read")]
        public async Task<ActionResult> Find(SupplierIdViewModel viewModel)
        {
            Supplier supplier = await Tenant.Suppliers
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new SupplierJson(supplier);
        }

        [HttpPost, Route("suppliers"), Authorization(Scope = "suppliers.read")]
        public async Task<ActionResult> List(ListSuppliersViewModel viewModel)
        {
            ICollection<Supplier> suppliers = await Tenant.Suppliers
                .WhereNameContains(viewModel.Name.Words())
                .OrderedByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new SupplierListJson(suppliers);
        }

        [HttpPost, Route("suppliers/update"), Authorization(Scope = "suppliers.edit")]
        public async Task<ActionResult> Update(UpdateSupplierViewModel viewModel)
        {
            Supplier supplier = await Tenant.Suppliers
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (supplier == null)
            {
                return this.Error("Supplier not found.");
            }

            viewModel.MapTo(supplier);
            await Tenant.SaveChangesAsync();

            return new SupplierJson(supplier);
        }
    }
}
