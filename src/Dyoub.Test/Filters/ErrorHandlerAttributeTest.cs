// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Mvc;

namespace Dyoub.Test.Filters
{
    [TestClass]
    public class ErrorHandlerAttributeTest
    {
        private ExceptionContext filterContext;

        [TestInitialize]
        public void Initialize()
        {
            filterContext = new ExceptionContext(
                new ControllerContextFake(new Mock<ControllerBase>().Object),
                new Exception()
            );

            filterContext.HttpContext = new HttpContextWrapperFake();
        }

        [TestMethod]
        public void ErrorMustBeLogged()
        {
            LoggerFake logger = new LoggerFake();

            ErrorHandlerAttribute attribute = new ErrorHandlerAttribute(logger);
            attribute.OnException(filterContext);

            Assert.IsTrue(logger.ErrorRegistered);
            Assert.IsTrue(filterContext.ExceptionHandled);
            Assert.IsTrue(filterContext.Result is ViewResult);
            Assert.AreEqual(500, filterContext.HttpContext.Response.StatusCode);
        }
    }
}
