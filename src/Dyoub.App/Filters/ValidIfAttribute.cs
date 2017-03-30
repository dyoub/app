// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Filters
{
    public class ValidIfAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (bool)value;
        }
    }
}
