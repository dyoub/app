// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Commercial.Customers
{
    public class Customer : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternativePhoneNumber { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
