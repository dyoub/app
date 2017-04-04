// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Infrastructure.Security;
using Dyoub.Test.Contexts;
using Dyoub.Test.Contexts.Account.Autorization;
using Dyoub.Test.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace Dyoub.Test.Filters
{
    [TestClass]
    public class AuthorizationAttributeTest
    {
        private AuthorizationContext filterContext;
        private AuthorizationAttribute attribute;

        [TestInitialize]
        public void Initialize()
        {
            filterContext = new AuthorizationContext(
                new ControllerContextFake(new Mock<ControllerBase>().Object),
                new Mock<ActionDescriptor>().Object
            );

            filterContext.HttpContext = new HttpContextWrapperFake();
        }

        [TestMethod]
        public void ValidateEmptyToken()
        {
            filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("token", null));

            attribute = new AuthorizationAttribute(new InMemoryContext());
            attribute.OnAuthorization(filterContext);

            Assert.IsNull(filterContext.HttpContext.UserIdentity());
        }

        [TestMethod]
        public void ValidateInexistentToken()
        {
            filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("token", "TokenThatDoesNotExistInDatabase"));

            attribute = new AuthorizationAttribute(new InMemoryContext());
            attribute.OnAuthorization(filterContext);

            Assert.IsNull(filterContext.HttpContext.UserIdentity());
        }

        [TestMethod]
        public void ValidateValidToken()
        {
            ValidateValidTokenContext context = new ValidateValidTokenContext();

            filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("token", context.User.Token));

            attribute = new AuthorizationAttribute(context);
            attribute.OnAuthorization(filterContext);

            UserIdentity userIdentity = filterContext.HttpContext.UserIdentity();

            Assert.IsNotNull(userIdentity);
        }

        [TestMethod]
        public void AccessWithScopePermission()
        {
            AccessWithScopePermissionContext context = new AccessWithScopePermissionContext();

            filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("token", context.User.Token));

            attribute = new AuthorizationAttribute(context);
            attribute.Scope = context.TeamRule.Scope;
            attribute.OnAuthorization(filterContext);

            UserIdentity userIdentity = filterContext.HttpContext.UserIdentity();

            Assert.IsNotNull(userIdentity);
        }

        [TestMethod]
        public void AccessWithoutScopePermission()
        {
            AccessWithoutScopePermissionContext context = new AccessWithoutScopePermissionContext();

            filterContext.HttpContext.Request.Cookies.Add(new HttpCookie("token", context.User.Token));

            attribute = new AuthorizationAttribute(context);
            attribute.Scope = "required.scope";
            attribute.OnAuthorization(filterContext);

            UserIdentity userIdentity = filterContext.HttpContext.UserIdentity();

            Assert.IsNull(userIdentity);
        }
    }
}
