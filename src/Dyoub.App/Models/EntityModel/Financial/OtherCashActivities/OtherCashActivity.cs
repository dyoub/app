// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.App.Models.EntityModel.Financial.OtherCashActivities
{
    public class OtherCashActivity : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual Store Store { get; set; }
    }
}
