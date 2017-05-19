// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.RentPayments
{
    public static class RentPaymentQuery
    {
        public static IQueryable<RentPayment> ForRentContractId(this IQueryable<RentPayment> rentPayments, int saleOrderId)
        {
            return rentPayments.Where(rentPayment => rentPayment.RentContractId == saleOrderId);
        }
    }
}
