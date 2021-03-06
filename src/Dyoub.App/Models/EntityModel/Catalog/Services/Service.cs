﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Catalog.Services
{
    public class Service : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool CanFraction { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<SaleService> SaleServices { get; set; }
        public virtual ICollection<ServicePrice> ServicePrices { get; set; }
    }
}
