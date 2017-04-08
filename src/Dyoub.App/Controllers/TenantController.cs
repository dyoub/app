// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel;
using System.Web.Mvc;

namespace Dyoub.App.Controllers
{
    public class TenantController : Controller
    {
        public TenantContext Tenant { get; private set; }

        public TenantController() { }

        public TenantController(TenantContext tenant)
        {
            Tenant = tenant;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Tenant = new TenantContext(filterContext.HttpContext.TenantId());
        }
    }
}
