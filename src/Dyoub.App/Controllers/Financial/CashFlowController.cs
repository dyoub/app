// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses;
using Dyoub.App.Models.EntityModel.Financial.RentIncomes;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Models.ViewModel.Financial.CashFlow;
using Dyoub.App.Results.Financial.CashFlow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Financial
{
    public class CashFlowController : TenantController
    {
        public CashFlowController() { }

        public CashFlowController(TenantContext tenant) : base(tenant) { }

        private IQueryable<CashActivity> CashActivities(int? storeId, DateTime fromDate, DateTime toDate)
        {
            IQueryable<CashActivity> rentIncomes = Tenant.RentIncomes
                .WhereStoreId(storeId)
                .WhereReceivedDateStartAt(fromDate)
                .WhereReceivedDateEndAt(toDate)
                .AsCashActivity();

            IQueryable<CashActivity> saleIncomes = Tenant.SaleIncomes
                .WhereStoreId(storeId)
                .WhereReceivedDateStartAt(fromDate)
                .WhereReceivedDateEndAt(toDate)
                .AsCashActivity();

            IQueryable<CashActivity> purchaseExpenses = Tenant.PurchaseExpenses
                .WhereStoreId(storeId)
                .WhereReceivedDateStartAt(fromDate)
                .WhereReceivedDateEndAt(toDate)
                .AsCashActivity();

            IQueryable<CashActivity> fixedExpenses = Tenant.FixedExpenses
                .WhereStoreId(storeId)
                .WhereEffectiveFrom(fromDate)
                .WhereEffectiveUntil(toDate)
                .AsCashActivity();

            IQueryable<CashActivity> others = Tenant.OtherCashActivities
                .WhereStoreId(storeId)
                .FromDate(fromDate)
                .UntilDate(toDate)
                .AsCashActivity();

            return saleIncomes
                .Concat(rentIncomes)
                .Concat(purchaseExpenses)
                .Concat(fixedExpenses)
                .Concat(others);
        }

        [HttpGet, Route("cash-flow/daily"), Authorization(Scope = "cash-flow.read")]
        public ActionResult Daily()
        {
            return View("~/Views/Financial/CashFlow/DailyCashFlow.cshtml");
        }

        [HttpGet, Route("cash-flow/monthly"), Authorization(Scope = "cash-flow.read")]
        public ActionResult Monthly()
        {
            return View("~/Views/Financial/CashFlow/MonthlyCashFlow.cshtml");
        }

        [HttpPost, Route("cash-flow/daily"), Authorization(Scope = "cash-flow.read")]
        public async Task<ActionResult> Daily(DailyCashFlowViewModel viewModel)
        {
            IEnumerable<CashActivity> cashActivities = await CashActivities(viewModel.StoreId,
                viewModel.FromDate, viewModel.ToDate).ToListAsync();

            CashFlowAnalysis cashFlowAnalysis = new CashFlowAnalysis(cashActivities);
            cashFlowAnalysis.ToPeriod(viewModel.FromDate, viewModel.ToDate);

            return new DailyCashFlowListJson(cashFlowAnalysis);
        }

        [HttpPost, Route("cash-flow/monthly"), Authorization(Scope = "cash-flow.read")]
        public async Task<ActionResult> Monthly(MonthlyCashFlowViewModel viewModel)
        {
            IEnumerable<CashActivity> cashActivities = await CashActivities(viewModel.StoreId,
                viewModel.FromDate, viewModel.ToDate).ToListAsync();

            CashFlowAnalysis cashFlowAnalysis = new CashFlowAnalysis(cashActivities);
            cashFlowAnalysis.ToPeriod(viewModel.FromDate, viewModel.ToDate);

            return new MonthlyCashFlowListJson(cashFlowAnalysis);
        }
    }
}
