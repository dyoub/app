// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Models.ServiceModel.Inventory;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.OrderProcessing
{
    public class PurchaseOrderProcessing
    {
        public TenantContext Tenant { get; private set; }
        public PurchaseOrder PurchaseOrder { get; private set; }
        public ProductReplenishment ProductReplenishment { get; private set; }
        public PurchaseCost PurchaseCost { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public PurchaseOrderProcessing(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private bool PurchaseOrderIsPending()
        {
            HasNoItems = PurchaseOrder.TotalPayable == 0;
            HasPendingPayment = PurchaseOrder.TotalPayable != PurchaseOrder.PurchasePayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        public async Task<bool> Confirm(int saleOrderId)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludePurchasePayments()
                .IncludePurchasedProducts()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || PurchaseOrder.Confirmed || PurchaseOrderIsPending())
            {
                return false;
            }
            
            ProductReplenishment = new ProductReplenishment(Tenant, PurchaseOrder);
            if (!await ProductReplenishment.Confirm()) return false;

            PurchaseCost = new PurchaseCost(Tenant, PurchaseOrder);
            PurchaseCost.Confirm();

            PurchaseOrder.ConfirmationDate = DateTime.Now;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int saleOrderId)
        {
            PurchaseOrder = await Tenant.PurchaseOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludePurchasedProducts()
                .IncludePurchaseExpenses()
                .SingleOrDefaultAsync();

            if (PurchaseOrder == null || !PurchaseOrder.Confirmed)
            {
                return false;
            }

            ProductReplenishment = new ProductReplenishment(Tenant, PurchaseOrder);
            await ProductReplenishment.Revert();

            PurchaseCost = new PurchaseCost(Tenant, PurchaseOrder);
            PurchaseCost.Revert();

            PurchaseOrder.ConfirmationDate = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
