// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dyoub.App.Extensions
{
    public static class StringExtensions
    {
        public static bool In(this string str, params string[] strs)
        {
            return strs.Contains(str, StringComparer.OrdinalIgnoreCase);
        }

        public static string NoAccents(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            str = str.Normalize(NormalizationForm.FormD);
            char[] chars = str.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string NoWhiteSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace(" ", string.Empty);
        }

        public static string OnlyNumbers(this string str)
        {
            return Regex.Replace(str, @"[^\d]", string.Empty);
        }

        public static string ToSnakeCase(this string str)
        {
            string result = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (i > 0 && char.IsUpper(str[i]) && !char.IsUpper(str[i - 1]))
                {
                    result += "_";
                }

                result += str[i].ToString();
            }

            return result.ToLower();
        }

        public static string[] Words(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new string[] { };
            }

            return str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
