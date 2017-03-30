// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Results.Common;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Dyoub.Test.Filters
{
    [TestClass]
    public class ModelStateAttributeTest
    {
        private ActionExecutingContext filterContext;
        private ModelStateAttribute attribute;

        [TestInitialize]
        public void Initialize()
        {
            filterContext = new ActionExecutingContext();
            filterContext.HttpContext = new HttpContextWrapperFake();
            filterContext.Controller = new Mock<ControllerBase>().Object;

            attribute = new ModelStateAttribute();
        }

        [TestMethod]
        public void ModelStateForInvalidModel()
        {
            filterContext.Controller.ViewData.ModelState.AddModelError("Error", "Error for testing");

            attribute.OnActionExecuting(filterContext);

            ActionResult result = filterContext.Result;

            Assert.AreEqual(422, filterContext.HttpContext.Response.StatusCode);
            Assert.IsTrue(result is ModelErrorsJson);
        }

        [TestMethod]
        public void ModelStateForValidModel()
        {
            attribute.OnActionExecuting(filterContext);

            JsonResult result = filterContext.Result as JsonResult;

            Assert.AreEqual(200, filterContext.HttpContext.Response.StatusCode);
        }
    }
}
