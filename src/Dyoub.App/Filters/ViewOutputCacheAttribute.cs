// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Web.Mvc;

namespace Dyoub.App.Filters
{
    public class ViewOutputCacheAttribute : OutputCacheAttribute
    {
        public ViewOutputCacheAttribute()
        {
            CacheProfile = "Default";
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                if (filterContext.RequestContext.HttpContext.Response.StatusCode != 500)
                {
                    base.OnActionExecuting(filterContext);
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                if (filterContext.RequestContext.HttpContext.Response.StatusCode != 500)
                {
                    base.OnActionExecuted(filterContext);
                }
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                if (filterContext.RequestContext.HttpContext.Response.StatusCode != 500)
                {
                    base.OnResultExecuting(filterContext);
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Result is ViewResult)
            {
                if (filterContext.RequestContext.HttpContext.Response.StatusCode != 500)
                {
                    base.OnResultExecuted(filterContext);
                }
            }
        }
    }
}
