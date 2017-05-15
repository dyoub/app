// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using System;

namespace Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses
{
    public class PurchaseExpense : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PurchasePaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual PurchasePayment PurchasePayment { get; set; }
    }
}
