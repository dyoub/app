// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.ViewModel.Financial.FixedExpenses;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Financial.FixedExpenses;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Financial
{
    public class FixedExpensesController : TenantController
    {
        public FixedExpensesController() { }

        public FixedExpensesController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("fixed-expenses/new"), Authorization(Scope = "fixed-expenses.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Financial/FixedExpenses/FixedExpenseEdit.cshtml");
        }

        [HttpGet, Route("fixed-expenses/details/{id:int}"), Authorization(Scope = "fixed-expenses.read")]
        public ActionResult Details()
        {
            return View("~/Views/Financial/FixedExpenses/FixedExpenseDetails.cshtml");
        }

        [HttpGet, Route("fixed-expenses/edit/{id:int}"), Authorization(Scope = "fixed-expenses.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Financial/FixedExpenses/FixedExpenseEdit.cshtml");
        }

        [HttpGet, Route("fixed-expenses"), Authorization(Scope = "fixed-expenses.read")]
        public ActionResult Index()
        {
            return View("~/Views/Financial/FixedExpenses/FixedExpenseList.cshtml");
        }

        [HttpPost, Route("fixed-expenses/create"), Authorization(Scope = "fixed-expenses.edit")]
        public async Task<ActionResult> Create(CreateFixedExpenseViewModel viewModel)
        {
            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            Tenant.FixedExpenses.Add(viewModel.MapTo(new FixedExpense()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("fixed-expenses/delete"), Authorization(Scope = "fixed-expenses.edit")]
        public async Task<ActionResult> Delete(FixedExpenseIdViewModel viewModel)
        {
            FixedExpense fixedExpense = await Tenant.FixedExpenses
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (fixedExpense != null)
            {
                Tenant.FixedExpenses.Remove(fixedExpense);
                await Tenant.SaveChangesAsync();
            }

            return this.Success();
        }

        [HttpPost, Route("fixed-expenses/find"), Authorization(Scope = "fixed-expenses.read")]
        public async Task<ActionResult> Find(FixedExpenseIdViewModel viewModel)
        {
            FixedExpense fixedExpense = await Tenant.FixedExpenses
                .IncludeStore()
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new FixedExpenseJson(fixedExpense);
        }

        [HttpPost, Route("fixed-expenses"), Authorization(Scope = "fixed-expenses.read")]
        public async Task<ActionResult> List(ListFixedExpensesViewModel viewModel)
        {
            ICollection<FixedExpense> fixedExpense = await Tenant.FixedExpenses
                .WhereStoreId(viewModel.StoreId)
                .WhereDescriptionContains(viewModel.Description.Words())
                .WhereEffectiveFrom(viewModel.StartDate)
                .WhereEffectiveUntil(viewModel.EndDate)
                .OrderByDescription()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new FixedExpenseListJson(fixedExpense);
        }

        [HttpPost, Route("fixed-expenses/update"), Authorization(Scope = "fixed-expenses.edit")]
        public async Task<ActionResult> Update(UpdateFixedExpenseViewModel viewModel)
        {
            if (!await Tenant.Stores.WhereId(viewModel.StoreId.Value).AnyAsync())
            {
                return this.Error("Store not found.");
            }

            FixedExpense fixedExpense = await Tenant.FixedExpenses
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (fixedExpense == null)
            {
                return this.Error("Fixed expense not found.");
            }

            viewModel.MapTo(fixedExpense);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
