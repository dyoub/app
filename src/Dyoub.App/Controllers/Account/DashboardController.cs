// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Results.Account.Dashboard;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Account
{
    public class DashboardController : Controller
    {
        [HttpGet]
        [Route("dashboard")]
        [Authorization]
        public ActionResult Overview()
        {
            return View("~/Views/Account/Dashboard/Overview.cshtml");
        }

        [HttpPost]
        [Route("dashboard/load")]
        [Authorization]
        public async Task<ActionResult> Load()
        {
            // TODO
            return new OverviewJson
            {
                UserIdentity = HttpContext.UserIdentity()
            };
        }
    }
}
