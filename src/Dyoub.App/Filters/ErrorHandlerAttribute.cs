// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Infrastructure.Logging;
using System;
using System.Web.Mvc;

namespace Dyoub.App.Filters
{
    public class ErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        private Logger logger;

        public ErrorHandlerAttribute() : this(new BackgroundLogger()) { }

        public ErrorHandlerAttribute(Logger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                Exception ex = filterContext.Exception;

                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                logger.Register(ex);

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Errors/InternalError.cshtml"
                };
            }
        }
    }
}
