// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Web.Mvc;

namespace Dyoub.App.Binders
{
    public class StringTrimModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (result == null)
            {
                return null;
            }

            return result.AttemptedValue.Trim();
        }
    }
}
