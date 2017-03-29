// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Dyoub.App.Extensions
{
    public static class HttpRequestBaseExtensions
    {
        public static string AccessToken(this HttpRequestBase request)
        {
            HttpCookie cookie = request.Cookies["token"];

            if (cookie == null || cookie.Value == null)
            {
                return null;
            }

            string token = cookie.Value;

            if (!Regex.IsMatch(token, "^[0-9A-F]$"))
            {
                return null;
            }

            return token;
        }

        public static string DomainUrl(this HttpRequestBase request)
        {
            return request.Url.Scheme + Uri.SchemeDelimiter + request.Url.Authority;
        }
    }
}
