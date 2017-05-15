// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Models.ServiceModel.Inventory;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.OrderProcessing
{
    public class SaleOrderProcessing
    {
        public TenantContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public ProductConsumption ProductConsumption { get; private set; }
        public Billing Billing { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public SaleOrderProcessing(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private bool SaleOrderIsPending()
        {
            HasNoItems = SaleOrder.TotalPayable == 0;
            HasPendingPayment = SaleOrder.TotalPayable != SaleOrder.SalePayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        public async Task<bool> Confirm(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludeSaleProducts()
                .IncludePaymentMethodsAndFees()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || SaleOrder.Confirmed || SaleOrderIsPending())
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, SaleOrder);
            if (!await ProductConsumption.Confirm()) return false;

            Billing = new Billing(Tenant, SaleOrder);
            Billing.Confirm();

            SaleOrder.ConfirmationDate = DateTime.Now;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeStore()
                .IncludeSaleProducts()
                .IncludeSaleIncomes()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || !SaleOrder.Confirmed)
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, SaleOrder);
            await ProductConsumption.Revert();

            Billing = new Billing(Tenant, SaleOrder);
            Billing.Revert();

            SaleOrder.ConfirmationDate = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
