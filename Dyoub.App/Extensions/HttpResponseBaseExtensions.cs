// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Web;

namespace Dyoub.App.Extensions
{
    public static class HttpResponseBaseExtensions
    {
        public static void ClearAccessToken(this HttpResponseBase response)
        {
            response.SetCookie(new HttpCookie("token")
            {
                Value = null,
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.Today.AddDays(-1)
            });
        }

        public static void SetAccessToken(this HttpResponseBase response, string token)
        {
            response.SetCookie(new HttpCookie("token")
            {
                Value = token,
                Path = "/",
                HttpOnly = true,
                Expires = DateTime.MinValue
            });
        }
    }
}