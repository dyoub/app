// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using System;

namespace Dyoub.App.Models.EntityModel.Financial.SaleIncomes
{
    public class SaleIncome : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int SalePaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public decimal AmountReceived { get; set; }
        public DateTime? AnticipationDate { get; set; }
        public decimal? AmountAnticipated { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual SalePayment SalePayment { get; set; }
    }
}
