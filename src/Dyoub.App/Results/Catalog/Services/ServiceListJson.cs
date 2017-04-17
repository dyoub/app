// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Catalog.Services
{
    public class ServiceListJson : JsonResult
    {
        public IEnumerable<Service> Services { get; private set; }

        public ServiceListJson(IEnumerable<Service> services)
        {
            Services = services;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Services.Select(service => new
            {
                id = service.Id,
                name = service.Name,
                code = service.Code,
                marketed = service.Marketed,
                additionalInformation = service.AdditionalInformation
            });

            base.ExecuteResult(context);
        }
    }
}
