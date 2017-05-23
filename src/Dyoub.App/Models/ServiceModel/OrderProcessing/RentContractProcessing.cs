// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.ServiceModel.Financial;
using Dyoub.App.Models.ServiceModel.Inventory;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.OrderProcessing
{
    public class RentContractProcessing
    {
        public TenantContext Tenant { get; private set; }
        public RentContract RentContract { get; private set; }
        public ProductConsumption ProductConsumption { get; private set; }
        public RentBilling Billing { get; private set; }
        public bool HasNoItems { get; private set; }
        public bool HasPendingPayment { get; private set; }

        public RentContractProcessing(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private bool RentContractIsPending()
        {
            HasNoItems = RentContract.TotalPayable == 0;
            HasPendingPayment = RentContract.TotalPayable != RentContract.RentPayments.Sum(p => p.Total);

            return HasNoItems || HasPendingPayment;
        }

        public async Task<bool> Confirm(int rentContractId)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeStore()
                .IncludeRentedProducts()
                .IncludePaymentMethodsAndFees()
                .SingleOrDefaultAsync();

            if (RentContract == null || RentContract.Confirmed || RentContractIsPending())
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, RentContract);
            if (!await ProductConsumption.Confirm()) return false;

            Billing = new RentBilling(Tenant, RentContract);
            Billing.Confirm();

            RentContract.ConfirmationDate = DateTime.UtcNow;

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Revert(int rentContractId)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeStore()
                .IncludeRentedProducts()
                .IncludeRentIncomes()
                .SingleOrDefaultAsync();

            if (RentContract == null || !RentContract.Confirmed)
            {
                return false;
            }

            ProductConsumption = new ProductConsumption(Tenant, RentContract);
            await ProductConsumption.Revert();

            Billing = new RentBilling(Tenant, RentContract);
            Billing.Revert();

            RentContract.ConfirmationDate = null;

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
