// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System;

namespace Dyoub.App.Models.EntityModel.Commercial.SalePayments
{
    public class SalePayment : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int SaleOrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public decimal InstallmentBilling { get; set; }
        public decimal? FeePercentage { get; set; }
        public decimal? FeeFixedValue { get; set; }
        public decimal Total { get; set; }
        public decimal BilledAmount { get; set; }
        public DateTime Date { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
    }
}
