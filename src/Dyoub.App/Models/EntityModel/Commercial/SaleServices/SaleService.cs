﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleServices
{
    public class SaleService : ITenantData
    {
        public int SaleOrderId { get; set; }
        public int ServiceId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }

        public virtual SaleOrder SaleOrder { get; set; }
        public virtual Service Service { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
