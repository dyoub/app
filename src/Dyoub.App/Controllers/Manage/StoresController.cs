﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Manage.Stores;
using Dyoub.App.Results.Manage.Stores;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Manage
{
    public class StoresController : TenantController
    {
        public StoresController() { }

        public StoresController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("stores/new"), Authorization(Scope = "stores.edit")]
        public ActionResult New()
        {
            return View("~/Views/Manage/Stores/StoreEdit.cshtml");
        }

        [HttpGet, Route("stores/edit/{id:int}"), Authorization(Scope = "stores.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Manage/Stores/StoreEdit.cshtml");
        }

        [HttpGet, Route("stores/details/{id:int}"), Authorization(Scope = "stores.read")]
        public ActionResult Details()
        {
            return View("~/Views/Manage/Stores/StoreDetails.cshtml");
        }

        [HttpGet, Route("stores"), Authorization(Scope = "stores.read")]
        public ActionResult Index()
        {
            return View("~/Views/Manage/Stores/StoreList.cshtml");
        }

        [HttpPost, Route("stores/create"), Authorization(Scope = "stores.edit")]
        public async Task<ActionResult> Create(CreateStoreViewModel viewModel)
        {
            Tenant.Stores.Add(viewModel.MapTo(new Store()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("stores/delete"), Authorization(Scope = "stores.edit")]
        public async Task<ActionResult> Delete(StoreIdViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (store == null)
            {
                return this.Error("Store not found.");
            }

            if (await Tenant.SaleOrders.WhereStoreId(store.Id).AnyAsync())
            {
                return this.Error("This store has associated sale orders.");
            }

            if (await Tenant.FixedExpenses.WhereStoreId(store.Id).AnyAsync())
            {
                return this.Error("This store has associated fixed expenses.");
            }

            if (await Tenant.OtherCashActivities.WhereStoreId(store.Id).AnyAsync())
            {
                return this.Error("This store has other associated cash activities.");
            }

            Tenant.Stores.Remove(store);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("stores/find"), Authorization(Scope = "stores.read")]
        public async Task<ActionResult> Find(StoreIdViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new StoreJson(store);
        }

        [HttpPost, Route("stores"), Authorization(Scope = "stores.read")]
        public async Task<ActionResult> List(ListStoresViewModel viewModel)
        {
            ICollection<Store> stores = await Tenant.Stores
                .WhereNameContains(viewModel.Name.Words())
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new StoreListJson(stores);
        }

        [HttpPost, Route("stores/update"), Authorization(Scope = "stores.edit")]
        public async Task<ActionResult> Update(UpdateStoreViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (store == null)
            {
                return this.Error("Store not found.");
            }

            viewModel.MapTo(store);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
