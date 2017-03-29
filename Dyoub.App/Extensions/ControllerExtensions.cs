// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Results.Common;
using System.Web.Mvc;

namespace Dyoub.App.Extensions
{
    public static class ControllerExtensions
    {
        public static JsonResult Error(this Controller controller, params string[] errors)
        {
            return new ModelErrorsJson(errors);
        }

        public static HttpStatusCodeResult AccessDenied(this Controller controller)
        {
            return new AccessDeniedStatusCode();
        }

        public static HttpStatusCodeResult NotFound(this Controller controller)
        {
            return new NotFoundStatusCode();
        }

        public static HttpStatusCodeResult Success(this Controller controller)
        {
            return new SuccessStatusCode();
        }

        public static HttpStatusCodeResult Unauthorized(this Controller controller)
        {
            return new UnauthorizedStatusCode();
        }

        public static string ViewToString(this Controller controller, string viewName)
        {
            return ViewToString(controller, viewName, null);
        }

        public static string ViewToString(this Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            StringView view = new StringView(
                context: controller.ControllerContext,
                viewData: controller.ViewData,
                tempData: controller.TempData,
                viewName: viewName
            );

            return view.ToString();
        }
    }
}
