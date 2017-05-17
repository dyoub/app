// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToJson(this DateTime dateTime)
        {
            return string.Format("{0:yyyy}-{0:MM}-{0:dd}T{0:HH}:{0:mm}:{0:ss}.{0:fff}", dateTime);
        }

        public static string ToJsonLocalTimeZone(this DateTime dateTime)
        {
            return string.Format("{0}Z", dateTime.ToJson());
        }

        public static string ToJson(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            return dateTime.Value.ToJson();
        }

        public static string ToJsonLocalTimeZone(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            return dateTime.Value.ToJsonLocalTimeZone();
        }
    }
}
