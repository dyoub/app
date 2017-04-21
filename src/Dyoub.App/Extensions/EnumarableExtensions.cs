// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Extensions
{
    public static class EnumarableExtensions
    {
        public static bool HasDuplicate<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> key)
        {
            return source.GroupBy(key).Any(g => g.Count() > 1);
        }
    }
}
