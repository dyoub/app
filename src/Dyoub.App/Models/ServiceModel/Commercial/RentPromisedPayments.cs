// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Financial;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Commercial
{
    public class RentPromisedPayments
    {
        public TenantContext Tenant { get; private set; }
        public RentContract RentContract { get; private set; }
        public bool HasPendingPayment { get; set; }

        public RentPromisedPayments(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private void CalculateTotals(IEnumerable<RentPayment> payments, decimal? discount)
        {
            decimal totalPaid = payments.Sum(payment => payment.Total);

            RentContract.Discount = discount;
            RentContract.TotalPayable = new Money(RentContract.Total)
                .SubtractPercentage(RentContract.Discount ?? 0);

            HasPendingPayment = totalPaid != RentContract.TotalPayable;
        }

        public async Task<bool> RegisterPayments(int rentContractId, IEnumerable<RentPayment> payments, decimal? discount)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeRentPayments()
                .SingleOrDefaultAsync();

            if (RentContract == null || RentContract.Confirmed)
            {
                return false;
            }

            CalculateTotals(payments, discount);

            if (HasPendingPayment)
            {
                return false;
            }

            Tenant.RentPayments.RemoveRange(RentContract.RentPayments);
            Tenant.RentPayments.AddRange(payments);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
