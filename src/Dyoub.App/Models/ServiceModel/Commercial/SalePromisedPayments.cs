// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Financial;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Commercial
{
    public class SalePromisedPayments
    {
        public TenantContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public bool HasPendingPayment { get; set; }

        public SalePromisedPayments(TenantContext tenant)
        {
            Tenant = tenant;
        }
        
        private void CalculateTotals(IEnumerable<SalePayment> payments, decimal? discount)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            SaleOrder.Discount = discount;
            SaleOrder.TotalPayable = new Money(SaleOrder.Total)
                .SubtractPercentage(SaleOrder.Discount ?? 0);

            HasPendingPayment = totalPaid != SaleOrder.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int saleOrderId, IEnumerable<SalePayment> payments, decimal? discount)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeSalePayments()
                .SingleOrDefaultAsync();

            if (SaleOrder == null || SaleOrder.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments, discount);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.SalePayments.RemoveRange(SaleOrder.SalePayments);
            Tenant.SalePayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
