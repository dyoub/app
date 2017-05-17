// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Results.Inventory.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Inventory
{
    public class PurchaseOrdersController : TenantController
    {
        public PurchaseOrdersController() { }

        public PurchaseOrdersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("purchase-orders/new"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Inventory/PurchaseOrders/PurchaseOrderEdit.cshtml");
        }

        [HttpGet, Route("purchase-orders/details/{id:int}"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Inventory/PurchaseOrders/PurchaseOrderDetails.cshtml");
        }

        [HttpGet, Route("purchase-orders/edit/{id:int}"), Authorization(Scope = "purchase-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Inventory/PurchaseOrders/PurchaseOrderEdit.cshtml");
        }

        [HttpGet, Route("purchase-orders"), Authorization(Scope = "purchase-orders.read")]
        public ActionResult Index()
        {
            return View("~/Views/Inventory/PurchaseOrders/PurchaseOrderList.cshtml");
        }

        [HttpPost, Route("purchase-orders/create"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Create(CreatePurchaseOrderViewModel viewModel)
        {
            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            if (viewModel.WalletId != null)
            {
                if (!await Tenant.Wallets.WhereId(viewModel.WalletId.Value).AnyAsync())
                {
                    return this.Error("Wallet not found.");
                }
            }

            PurchaseOrder purchaseOrder = viewModel.MapTo(new PurchaseOrder());
            purchaseOrder.CreatedAt = DateTime.UtcNow;
            purchaseOrder.Author = HttpContext.UserIdentity().Email;

            Tenant.PurchaseOrders.Add(purchaseOrder);
            await Tenant.SaveChangesAsync();

            return new PurchaseOrderIdJson(purchaseOrder);
        }

        [HttpPost, Route("purchase-orders/delete"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Delete(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (purchaseOrder == null) return this.Error("Purchase order not found.");
            if (purchaseOrder.Confirmed) return this.Error("Cannot remove confirmed purchases orders.");

            Tenant.PurchaseOrders.Remove(purchaseOrder);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("purchase-orders/find"), Authorization(Scope = "purchase-orders.read")]
        public async Task<ActionResult> Find(PurchaseOrderIdViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .IncludeStore()
                .IncludeWallet()
                .SingleOrDefaultAsync();

            return new PurchaseOrderJson(purchaseOrder);
        }

        [HttpPost, Route("purchase-orders"), Authorization(Scope = "purchase-orders.read")]
        public async Task<ActionResult> List(ListPurchaseOrdersViewModel viewModel)
        {
            ICollection<PurchaseOrder> purchaseOrders = await Tenant.PurchaseOrders
                .WhereStoreId(viewModel.StoreId)
                .WhereIssueDateStartAt(viewModel.FromDate)
                .WhereIssueDateEndAt(viewModel.ToDate)
                .OrderByMostRecent()
                .IncludeStore()
                .IncludeSupplier()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new PurchaseOrderListJson(purchaseOrders);
        }

        [HttpPost, Route("purchase-orders/update"), Authorization(Scope = "purchase-orders.edit")]
        public async Task<ActionResult> Update(UpdatePurchaseOrderViewModel viewModel)
        {
            PurchaseOrder purchaseOrder = await Tenant.PurchaseOrders
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (purchaseOrder == null) return this.Error("Purchase order not found.");
            if (purchaseOrder.Confirmed) return this.Error("Cannot change confirmed purchases orders.");

            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            if (viewModel.WalletId != null)
            {
                if (!await Tenant.Wallets.WhereId(viewModel.WalletId.Value).AnyAsync())
                {
                    return this.Error("Wallet not found.");
                }
            }

            viewModel.MapTo(purchaseOrder);
            await Tenant.SaveChangesAsync();

            return new PurchaseOrderIdJson(purchaseOrder);
        }
    }
}
