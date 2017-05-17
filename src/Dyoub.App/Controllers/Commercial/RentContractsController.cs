// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentContractsController : TenantController
    {
        public RentContractsController() { }

        public RentContractsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/new"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Commercial/RentContracts/RentContractEdit.cshtml");
        }

        [HttpGet, Route("rent-contracts/details/{id:int}"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentContracts/RentContractDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentContracts/RentContractEdit.cshtml");
        }

        [HttpGet, Route("rent-contracts"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Index()
        {
            return View("~/Views/Commercial/RentContracts/RentContractList.cshtml");
        }

        [HttpPost, Route("rent-contracts/create"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Create(CreateRentContractViewModel viewModel)
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

            RentContract rentContract = viewModel.MapTo(new RentContract());
            rentContract.CreatedAt = DateTime.UtcNow;
            rentContract.Author = HttpContext.UserIdentity().Email;

            Tenant.RentContracts.Add(rentContract);
            await Tenant.SaveChangesAsync();

            return new RentContractIdJson(rentContract);
        }

        [HttpPost, Route("rent-contracts/delete"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Delete(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (rentContract == null) return this.Error("Rent contract not found.");
            if (rentContract.Confirmed) return this.Error("Cannot remove confirmed sales orders.");

            Tenant.RentContracts.Remove(rentContract);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("rent-contracts/find"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> Find(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .IncludeStore()
                .IncludeWallet()
                .SingleOrDefaultAsync();

            return new RentContractJson(rentContract);
        }

        [HttpPost, Route("rent-contracts"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> List(ListRentContractsViewModel viewModel)
        {
            ICollection<RentContract> rentContracts = await Tenant.RentContracts
                .WhereStoreId(viewModel.StoreId)
                .WhereStartAt(viewModel.FromDate)
                .WhereEndAt(viewModel.ToDate)
                .OrderByMostRecent()
                .IncludeStore()
                .IncludeCustomer()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new RentContractListJson(rentContracts);
        }

        [HttpPost, Route("rent-contracts/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(UpdateRentContractViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (rentContract == null) return this.Error("Rent contract not found.");
            if (rentContract.Confirmed) return this.Error("Cannot change confirmed sales orders.");

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

            viewModel.MapTo(rentContract);
            await Tenant.SaveChangesAsync();

            return new RentContractIdJson(rentContract);
        }
    }
}
