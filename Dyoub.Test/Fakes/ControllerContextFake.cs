// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Moq;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dyoub.Test.Fakes
{
    public class ControllerContextFake : ControllerContext
    {
        public ControllerContextFake(Controller controller)
            : base(HttpContextBaseFake(), RouteDataFake(), controller)
        {
            SetupViewEngine();
        }

        private static void SetupViewEngine()
        {
            Mock<IViewEngine> viewEngine = new Mock<IViewEngine>();
            Mock<IView> view = new Mock<IView>();

            ViewEngineResult viewEngineResult = new ViewEngineResult(view.Object, viewEngine.Object);
            
            viewEngine.Setup(p => p.FindPartialView(It.IsAny<ControllerContext>(), It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(viewEngineResult);
            
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine.Object);
        }

        private static HttpContextBase HttpContextBaseFake()
        {
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            Mock<HttpResponseBase> response = new Mock<HttpResponseBase>();

            context.SetupGet(p => p.Response).Returns(response.Object);
            context.SetupGet(p => p.Items).Returns(new ListDictionary());

            return context.Object;
        }

        private static RouteData RouteDataFake()
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "test");

            return routeData;
        }
    }
}
