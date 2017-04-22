// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.ViewModel.Financial.OtherCashActivities;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Financial.OtherCashActivities;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Financial
{
    public class OtherCashActivitiesController : TenantController
    {
        public OtherCashActivitiesController() { }

        public OtherCashActivitiesController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("other-cash-activities/new"), Authorization(Scope = "other-cash-activities.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Financial/OtherCashActivities/OtherCashActivityEdit.cshtml");
        }

        [HttpGet, Route("other-cash-activities/details/{id:int}"), Authorization(Scope = "other-cash-activities.read")]
        public ActionResult Details()
        {
            return View("~/Views/Financial/OtherCashActivities/OtherCashActivityDetails.cshtml");
        }

        [HttpGet, Route("other-cash-activities/edit/{id:int}"), Authorization(Scope = "other-cash-activities.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Financial/OtherCashActivities/OtherCashActivityEdit.cshtml");
        }

        [HttpGet, Route("other-cash-activities"), Authorization(Scope = "other-cash-activities.read")]
        public ActionResult Index()
        {
            return View("~/Views/Financial/OtherCashActivities/OtherCashActivityList.cshtml");
        }

        [HttpPost, Route("other-cash-activities/create"), Authorization(Scope = "other-cash-activities.edit")]
        public async Task<ActionResult> Create(CreateOtherCashActivityViewModel viewModel)
        {
            Store store = await Tenant.Stores
                .WhereId(viewModel.StoreId.Value)
                .SingleOrDefaultAsync();

            if (store == null)
            {
                return this.Error("Store not found.");
            }

            Tenant.Set<OtherCashActivity>().Add(viewModel.MapTo(new OtherCashActivity()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("other-cash-activities/delete"), Authorization(Scope = "other-cash-activities.edit")]
        public async Task<ActionResult> Delete(OtherCashActivityIdViewModel viewModel)
        {
            OtherCashActivity otherCashActivity = await Tenant.OtherCashActivities
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (otherCashActivity == null)
            {
                return this.Error("Other cash activity not found.");
            }

            Tenant.Set<OtherCashActivity>().Remove(otherCashActivity);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("other-cash-activities/find"), Authorization(Scope = "other-cash-activities.read")]
        public async Task<ActionResult> Find(OtherCashActivityIdViewModel viewModel)
        {
            OtherCashActivity otherCashActivity = await Tenant.OtherCashActivities
                .WhereId(viewModel.Id.Value)
                .IncludeStore()
                .SingleOrDefaultAsync();

            return new OtherCashActivityJson(otherCashActivity);
        }

        [HttpPost, Route("other-cash-activities"), Authorization(Scope = "other-cash-activities.read")]
        public async Task<ActionResult> List(ListOtherCashActivitiesViewModel viewModel)
        {
            ICollection<OtherCashActivity> otherCashActivities = await Tenant.OtherCashActivities
                .IncludeStore()
                .WhereStoreId(viewModel.StoreId)
                .WhereDescriptionContains(viewModel.Description.Words())
                .FromDate(viewModel.StartDate)
                .UntilDate(viewModel.EndDate)
                .OrderByDescription()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new OtherCashActivityListJson(otherCashActivities);
        }

        [HttpPost, Route("other-cash-activities/update"), Authorization(Scope = "other-cash-activities.edit")]
        public async Task<ActionResult> Update(UpdateOtherCashActivityViewModel viewModel)
        {
            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            OtherCashActivity otherCashActivity = await Tenant.OtherCashActivities
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (otherCashActivity == null)
            {
                return this.Error("Other cash activity not found.");
            }

            viewModel.MapTo(otherCashActivity);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
