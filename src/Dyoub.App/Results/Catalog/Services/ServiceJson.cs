// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Services;
using System.Web.Mvc;

namespace Dyoub.App.Results.Catalog.Services
{
    public class ServiceJson : JsonResult
    {
        public Service Service { get; private set; }

        public ServiceJson(Service service)
        {
            Service = service;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Service == null ? null : new
            {
                id = Service.Id,
                name = Service.Name,
                code = Service.Code,
                marketed = Service.Marketed,
                canFraction = Service.CanFraction,
                additionalInformation = Service.AdditionalInformation
            };

            base.ExecuteResult(context);
        }
    }
}
