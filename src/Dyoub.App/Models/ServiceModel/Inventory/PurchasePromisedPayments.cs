// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using Dyoub.App.Models.EntityModel.Financial;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Inventory
{
    public class PurchasePromisedPayments
    {
        public TenantContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public bool HasPendingPayment { get; private set; }
        public decimal? ShippingCost { get; set; }
        public decimal? OtherTaxes { get; set; }
        public decimal? Discount { get; set; }

        public PurchasePromisedPayments(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private void CalculateTotals(IEnumerable<PurchasePayment> payments)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            PurchaseOrder.Discount = Discount;
            PurchaseOrder.ShippingCost = ShippingCost;
            PurchaseOrder.OtherTaxes = OtherTaxes;
            PurchaseOrder.TotalPayable = new Money(PurchaseOrder.Total)
                .Add(PurchaseOrder.ShippingCost ?? 0)
                .Add(PurchaseOrder.OtherTaxes ?? 0)
                .SubtractPercentage(PurchaseOrder.Discount ?? 0);

            HasPendingPayment = totalPaid != PurchaseOrder.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int purchaseOrderId, IEnumerable<PurchasePayment> payments)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(purchaseOrderId)
                .IncludePurchasePayments()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || PurchaseOrder.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.PurchasePayments.RemoveRange(PurchaseOrder.PurchasePayments);
            Tenant.PurchasePayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
