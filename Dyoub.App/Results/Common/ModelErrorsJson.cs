// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Web.Mvc;

namespace Dyoub.App.Results.Common
{
    public class ModelErrorsJson : JsonResult
    {
        public string[] Errors { get; private set; }

        public ModelErrorsJson(params string[] errors)
        {
            Errors = errors;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 422;

            Data = new
            {
                errors = Errors
            };

            base.ExecuteResult(context);
        }
    }
}