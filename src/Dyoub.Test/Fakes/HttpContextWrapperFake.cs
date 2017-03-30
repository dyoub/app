// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Moq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Dyoub.Test.Fakes
{
    public class HttpContextWrapperFake : HttpContextWrapper
    {
        public HttpContextWrapperFake() : base(HttpContext()) { }

        private static HttpContext HttpContext()
        {
            Mock<TextWriter> writer = new Mock<TextWriter>();

            HttpRequest request = new HttpRequest(string.Empty, "http://localhost/FakeURL", string.Empty);
            HttpResponse response = new HttpResponse(writer.Object);
            return new HttpContext(request, response);
        }
    }
}
