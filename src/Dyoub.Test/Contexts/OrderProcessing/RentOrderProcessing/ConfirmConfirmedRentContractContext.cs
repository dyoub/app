// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System;

namespace Dyoub.Test.Contexts.OrderProcessing.RentContractProcessing
{
    public class ConfirmConfirmedRentContractContext : TenantContext
    {
        private RentContract originial;
        private RentPayment rentPayment;

        public RentContract RentContract { get; private set; }

        public ConfirmConfirmedRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            RentContract = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store));
            RentContract.ConfirmationDate = DateTime.UtcNow.Date.AddDays(-1);

            rentPayment = RentPayments.Add(RentPaymentFactory.RentPayment(RentContract, paymentMethod));
            rentPayment.NumberOfInstallments = 2;
            rentPayment.InstallmentValue = rentPayment.Total / rentPayment.NumberOfInstallments;

            originial = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store));
            originial.ConfirmationDate = RentContract.ConfirmationDate;

            SaveChanges();
        }

        public bool RentContractWasNotConfirmedMoreThanOnce()
        {
            Entry(RentContract).Reload();
            return RentContract.ConfirmationDate == originial.ConfirmationDate;
        }
    }
}
