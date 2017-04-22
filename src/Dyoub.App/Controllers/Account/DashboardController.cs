// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Financial.CashActivities;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Overview;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Results.Account.Dashboard;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Account
{
    public class DashboardController : TenantController
    {
        public DashboardController() { }

        public DashboardController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("dashboard/account"), Authorization]
        public ActionResult Account()
        {
            return View("~/Views/Account/Dashboard/Account.cshtml");
        }

        [HttpGet, Route("dashboard/catalog"), Authorization]
        public ActionResult Catalog()
        {
            return View("~/Views/Account/Dashboard/Catalog.cshtml");
        }

        [HttpGet, Route("dashboard/commercial"), Authorization]
        public ActionResult Commercial()
        {
            return View("~/Views/Account/Dashboard/Commercial.cshtml");
        }

        [HttpGet, Route("dashboard/financial"), Authorization]
        public ActionResult Financial()
        {
            return View("~/Views/Account/Dashboard/Financial.cshtml");
        }

        [HttpGet, Route("dashboard/management"), Authorization]
        public ActionResult Management()
        {
            return View("~/Views/Account/Dashboard/Management.cshtml");
        }

        [HttpGet, Route("dashboard"), Authorization]
        public ActionResult General()
        {
            return View("~/Views/Account/Dashboard/General.cshtml");
        }
        
        [HttpGet, Route("dashboard/stock"), Authorization]
        public ActionResult Stock()
        {
            return View("~/Views/Account/Dashboard/Stock.cshtml");
        }

        [HttpPost, Route("dashboard/catalog"), Authorization]
        public async Task<ActionResult> CatalogCount()
        {
            CatalogCount counter = await Tenant.Current
                .Select(tenant => new CatalogCount
                {
                    Products = tenant.Products.Count(),
                    Services = tenant.Services.Count()
                })
                .SingleOrDefaultAsync();

            return new CatalogOverviewJson(counter);
        }

        [HttpPost, Route("dashboard/commercial"), Authorization]
        public async Task<ActionResult> CommercialCount()
        {
            CommercialCount counter = await Tenant.Current
                .Select(tenant => new CommercialCount
                {
                    Customers = tenant.Customers.Count(),
                    PaymentMethods = tenant.PaymentMethods.Count()
                })
                .SingleOrDefaultAsync();

            return new CommercialOverviewJson(counter);
        }

        [HttpPost, Route("dashboard/financial"), Authorization]
        public async Task<ActionResult> FinancialCount()
        {
            IQueryable<FixedExpense> fixedExpenses = Tenant.FixedExpenses.ToCurrentMonth();
            IQueryable<CashActivity> cashActivities = fixedExpenses.AsCashActivity();

            CashFlowAnalysis cashFlowAnalysis = new CashFlowAnalysis();
            cashFlowAnalysis.CashActivities = await cashActivities.ToListAsync();
            cashFlowAnalysis.ToCurrentMonth();
            
            return new FinancialOverviewJson(cashFlowAnalysis);
        }

        [HttpPost, Route("dashboard/management"), Authorization]
        public async Task<ActionResult> ManagementCount()
        {
            ManagementCount counter = await Tenant.Current
                .Select(tenant => new ManagementCount
                {
                    Stores = tenant.Stores.Count()
                })
                .SingleOrDefaultAsync();

            return new ManagementOverviewJson(counter);
        }
    }
}
