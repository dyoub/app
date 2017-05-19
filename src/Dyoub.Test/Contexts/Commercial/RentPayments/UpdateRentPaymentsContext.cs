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
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.RentPayments
{
    public class UpdateRentPaymentsContext : TenantContext
    {
        private RentPayment original;

        public RentPayment RentPayment { get; private set; }
        public PaymentMethod AnotherPaymentMethod { get; private set; }

        public UpdateRentPaymentsContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            RentContract rentContract = RentContracts.Add(RentContractFactory.RentContract(store));

            AnotherPaymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            RentPayment = RentPayments.Add(RentPaymentFactory.RentPayment(rentContract, paymentMethod));
            original = RentPayments.Add(RentPaymentFactory.RentPayment(rentContract, paymentMethod));

            SaveChanges();
        }

        public bool RentPaymentsWasUpdated()
        {
            RentPayment rentPayment = RentPayments.First();

            return rentPayment.Date != original.Date &&
                   rentPayment.NumberOfInstallments != original.NumberOfInstallments &&
                   rentPayment.InstallmentValue != original.InstallmentValue &&
                   rentPayment.PaymentMethodId != original.PaymentMethodId;
        }
    }
}
