// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasePayments
{
    public class PurchasePayment : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PurchaseOrderId { get; set; }
        public int NumberOfInstallments { get; set; }
        public decimal InstallmentValue { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
