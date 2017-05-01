// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Commercial.PaymentMethods
{
    public class PaymentMethod : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public int InstallmentLimit { get; set; }
        public int? DaysToReceive { get; set; }
        public bool EarlyReceipt { get; set; }
        public bool Active { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<PaymentMethodFee> PaymentMethodFees { get; set; }
        public virtual ICollection<SalePayment> SalePayments { get; set; }
    }
}
