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
using System;

namespace Dyoub.Test.Contexts.Commercial.SaleOrderBilling
{
    public class ConfirmConfirmedSaleOrderContext : TenantContext
    {
        private SaleOrder originial;
        private SalePayment salePayment;

        public SaleOrder SaleOrder { get; private set; }

        public ConfirmConfirmedSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));
            SaleOrder.ConfirmationDate = DateTime.Today.AddDays(-1);

            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;
            salePayment.InstallmentValue = salePayment.Total / salePayment.NumberOfInstallments;

            originial = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));
            originial.ConfirmationDate = SaleOrder.ConfirmationDate;

            SaveChanges();
        }

        public bool SaleOrderWasNotConfirmedAgain()
        {
            Entry(SaleOrder).Reload();
            return SaleOrder.ConfirmationDate == originial.ConfirmationDate;
        }
    }
}
