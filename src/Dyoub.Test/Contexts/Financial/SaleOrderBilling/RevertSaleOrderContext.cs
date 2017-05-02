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

namespace Dyoub.Test.Contexts.Commercial.SaleOrderBilling
{
    public class RevertSaleOrderContext : TenantContext
    {
        private SalePayment salePayment;

        public SaleOrder SaleOrder { get; private set; }

        public RevertSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));

            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;

            SaveChanges();
        }

        public bool SaleOrderWasReverted()
        {
            Entry(SaleOrder).Reload();
            Entry(salePayment).Reload();

            return SaleOrder.ConfirmationDate == null &&
                   SaleOrder.BilledAmount == 0 &&
                   salePayment.InstallmentBilling == 0 &&
                   salePayment.BilledAmount == 0 &&
                   !SaleIncomes.Any();
        }
    }
}
