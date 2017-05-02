// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.ViewModel.Financial.Wallets;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Financial.Wallets;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Financial
{
    public class WalletsController : TenantController
    {
        public WalletsController() { }

        public WalletsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("wallets/new"), Authorization(Scope = "wallets.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Financial/Wallets/WalletEdit.cshtml");
        }

        [HttpGet, Route("wallets/details/{id:int}"), Authorization(Scope = "wallets.read")]
        public ActionResult Details()
        {
            return View("~/Views/Financial/Wallets/WalletDetails.cshtml");
        }

        [HttpGet, Route("wallets/edit/{id:int}"), Authorization(Scope = "wallets.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Financial/Wallets/WalletEdit.cshtml");
        }

        [HttpGet, Route("wallets"), Authorization(Scope = "wallets.read")]
        public ActionResult Index()
        {
            return View("~/Views/Financial/Wallets/WalletList.cshtml");
        }

        [HttpPost, Route("wallets/create"), Authorization(Scope = "wallets.edit")]
        public async Task<ActionResult> Create(CreateWalletViewModel viewModel)
        {
            Tenant.Wallets.Add(viewModel.MapTo(new Wallet()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("wallets/delete"), Authorization(Scope = "wallets.edit")]
        public async Task<ActionResult> Delete(WalletIdViewModel viewModel)
        {
            Wallet wallet = await Tenant.Wallets
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (wallet == null)
            {
                return this.Error("Wallet not found.");
            }

            if (await Tenant.SaleOrders.WhereWalletId(wallet.Id).AnyAsync())
            {
                return this.Error("This wallet has associated sales orders.");
            }

            Tenant.Wallets.Remove(wallet);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("wallets/find"), Authorization(Scope = "wallets.read")]
        public async Task<ActionResult> Find(WalletIdViewModel viewModel)
        {
            Wallet wallet = await Tenant.Wallets
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new WalletJson(wallet);
        }

        [HttpPost, Route("wallets"), Authorization(Scope = "wallets.read")]
        public async Task<ActionResult> List(ListWalletsViewModel viewModel)
        {
            ICollection<Wallet> wallets = await Tenant.Wallets
                .WhereNameContains(viewModel.Name.Words())
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new WalletListJson(wallets);
        }

        [HttpPost, Route("wallets/actives"), Authorization(Scope = "wallets.read")]
        public async Task<ActionResult> ListActives()
        {
            ICollection<Wallet> wallets = await Tenant.Wallets
                .OrderByName()
                .ToListAsync();

            return new WalletListJson(wallets);
        }

        [HttpPost, Route("wallets/update"), Authorization(Scope = "wallets.edit")]
        public async Task<ActionResult> Update(UpdateWalletViewModel viewModel)
        {
            Wallet wallet = await Tenant.Wallets
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (wallet == null)
            {
                return this.Error("Wallet not found.");
            }

            viewModel.MapTo(wallet);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
