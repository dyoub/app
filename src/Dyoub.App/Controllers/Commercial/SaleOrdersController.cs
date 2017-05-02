// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class SaleOrdersController : TenantController
    {
        public SaleOrdersController() { }

        public SaleOrdersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("sale-orders/new"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Commercial/SaleOrders/SaleOrderEdit.cshtml");
        }

        [HttpGet, Route("sale-orders/details/{id:int}"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/SaleOrders/SaleOrderDetails.cshtml");
        }

        [HttpGet, Route("sale-orders/edit/{id:int}"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/SaleOrders/SaleOrderEdit.cshtml");
        }

        [HttpGet, Route("sale-orders"), Authorization(Scope = "sale-orders.read")]
        public ActionResult Index()
        {
            return View("~/Views/Commercial/SaleOrders/SaleOrderList.cshtml");
        }

        [HttpPost, Route("sale-orders/create"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Create(CreateSaleOrderViewModel viewModel)
        {
            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            SaleOrder saleOrder = viewModel.MapTo(new SaleOrder());
            saleOrder.CreatedAt = DateTime.Now;
            saleOrder.Author = HttpContext.UserIdentity().Email;

            Tenant.SaleOrders.Add(saleOrder);
            await Tenant.SaveChangesAsync();

            return new SaleOrderIdJson(saleOrder);
        }

        [HttpPost, Route("sale-orders/delete"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Delete(SaleOrderIdViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (saleOrder == null) return this.Error("Sale order not found.");
            if (saleOrder.Confirmed) return this.Error("Cannot remove confirmed sales orders.");

            Tenant.SaleOrders.Remove(saleOrder);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("sale-orders/find"), Authorization(Scope = "sale-orders.read")]
        public async Task<ActionResult> Find(SaleOrderIdViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .IncludeStore()
                .SingleOrDefaultAsync();

            return new SaleOrderJson(saleOrder);
        }

        [HttpPost, Route("sale-orders"), Authorization(Scope = "sale-orders.read")]
        public async Task<ActionResult> List(ListSaleOrdersViewModel viewModel)
        {
            ICollection<SaleOrder> saleOrders = await Tenant.SaleOrders
                .WhereStoreId(viewModel.StoreId)
                .WhereIssueDateStartAt(viewModel.FromDate)
                .WhereIssueDateEndAt(viewModel.ToDate)
                .OrderByMostRecent()
                .IncludeStore()
                .IncludeCustomer()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new SaleOrderListJson(saleOrders);
        }

        [HttpPost, Route("sale-orders/update"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Update(UpdateSaleOrderViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (saleOrder == null) return this.Error("Sale order not found.");
            if (saleOrder.Confirmed) return this.Error("Cannot change confirmed sales orders.");

            viewModel.MapTo(saleOrder);
            await Tenant.SaveChangesAsync();

            return new SaleOrderIdJson(saleOrder);
        }
    }
}
