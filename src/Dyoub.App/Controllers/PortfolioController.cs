// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Web.Mvc;

namespace Dyoub.App.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet, Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}
