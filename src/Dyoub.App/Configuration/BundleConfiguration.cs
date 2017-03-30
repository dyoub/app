// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Web.Optimization;

namespace Dyoub.App.Configuration
{
    public class BundleConfiguration
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Bundle")
                .IncludeDirectory("~/Scripts/modules", "*.js", true)
                .IncludeDirectory("~/Scripts/controllers", "*.js", true));

            BundleTable.EnableOptimizations = true;
        }
    }
}
