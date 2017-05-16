// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Binders;
using System;
using System.Web.Mvc;

namespace Dyoub.App.Configuration
{
    public class ModelBindersConfiguration
    {
        public static void RegisterModelBinders(ModelBinderDictionary binders)
        {
            binders.Add(typeof(string), new StringTrimModelBinder());
        }
    }
}
