// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.IO;
using System.Web.Mvc;

namespace Dyoub.App.Results.Common
{
    public class StringView
    {
        public ControllerContext Context { get; private set; }
        public ViewDataDictionary ViewData { get; private set; }
        public TempDataDictionary TempData { get; private set; }
        public string ViewName { get; private set; }
        
        public StringView(ControllerContext context, ViewDataDictionary viewData, TempDataDictionary tempData, string viewName)
        {
            Context = context;
            ViewData = viewData;
            TempData = tempData;
            ViewName = viewName;
        }

        public override string ToString()
        {
            using (StringWriter stringWriter = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(Context, ViewName);
                ViewContext viewContext = new ViewContext(Context, viewResult.View, ViewData, TempData, stringWriter);

                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(Context, viewResult.View);

                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
