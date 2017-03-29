// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Net;
using System.Web.Mvc;

namespace Dyoub.App.Results.Common
{
    public class NotFoundStatusCode : HttpStatusCodeResult
    {
        public NotFoundStatusCode() : base(HttpStatusCode.NotFound) { }
    }
}
