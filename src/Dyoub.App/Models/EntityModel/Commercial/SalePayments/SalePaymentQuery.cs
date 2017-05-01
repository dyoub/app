// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.SalePayments
{
    public static class SalePaymentQuery
    {
        public static IQueryable<SalePayment> ForSaleOrderId(this IQueryable<SalePayment> salePayments, int saleOrderId)
        {
            return salePayments.Where(salePayment => salePayment.SaleOrderId == saleOrderId);
        }
    }
}
