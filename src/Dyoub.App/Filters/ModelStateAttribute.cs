// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Results.Common;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Filters
{
    public class ModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.HttpContext.Response.StatusCode = 422;

                filterContext.Result = new ModelErrorsJson(
                    filterContext.Controller.ViewData.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray()
                );
            }
        }
    }
}
