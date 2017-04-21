// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;

namespace Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees
{
    public class PaymentMethodFee : ITenantData
    {
        public int PaymentMethodId { get; set; }
        public int TenantId { get; set; }
        public int MinimumInstallment { get; set; }
        public decimal? FeePercentage { get; set; }
        public decimal? FeeFixedValue { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
