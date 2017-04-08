// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel;
using System.Web.Mvc;

namespace Dyoub.App.Controllers
{
    public class TenantController : Controller
    {
        public TenantContext Context { get; private set; }

        public TenantController() { }

        public TenantController(TenantContext context)
        {
            Context = context;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Context = new TenantContext(filterContext.HttpContext.TenantId());
        }
    }
}
