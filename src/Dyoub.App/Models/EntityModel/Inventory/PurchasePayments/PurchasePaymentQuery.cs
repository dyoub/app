// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasePayments
{
    public static class PurchasePaymentQuery
    {
        public static IQueryable<PurchasePayment> ForPurchaseOrderId(this IQueryable<PurchasePayment> salePayments, int saleOrderId)
        {
            return salePayments.Where(salePayment => salePayment.PurchaseOrderId == saleOrderId);
        }
    }
}
