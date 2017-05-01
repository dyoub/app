// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.SalePayments
{
    public class UpdatePaymentsOfConfirmedSaleOrderContext : TenantContext
    {
        private SalePayment original;

        public SalePayment SalePayment { get; private set; }
        public PaymentMethod AnotherPaymentMethod { get; private set; }

        public UpdatePaymentsOfConfirmedSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            SaleOrder saleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));

            AnotherPaymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            SalePayment = SalePayments.Add(SalePaymentFactory.SalePayment(saleOrder, paymentMethod));
            original = SalePayments.Add(SalePaymentFactory.SalePayment(saleOrder, paymentMethod));

            SaveChanges();
        }

        public bool SalePaymentsNotChanged()
        {
            SalePayment salePayment = SalePayments.First();

            return salePayment.Date == original.Date &&
                   salePayment.NumberOfInstallments == original.NumberOfInstallments &&
                   salePayment.InstallmentValue == original.InstallmentValue &&
                   salePayment.PaymentMethodId == original.PaymentMethodId;
        }
    }
}
