// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.SaleOrderBilling
{
    public class ConfirmSaleOrderContext : TenantContext
    {
        private SalePayment salePayment;

        public SaleOrder SaleOrder { get; private set; }

        public ConfirmSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store));

            salePayment = SalePayments.Add(SalePaymentFactory.SalePayment(SaleOrder, paymentMethod));
            salePayment.NumberOfInstallments = 2;
            salePayment.InstallmentValue = salePayment.Total / salePayment.NumberOfInstallments;

            SaveChanges();
        }

        public bool SaleOrderWasConfirmed()
        {
            Entry(SaleOrder).Reload();

            SaleIncome[] incomes = SaleIncomes.ToArray();

            return SaleOrder.ConfirmationDate != null &&
                   SaleOrder.BilledAmount == salePayment.BilledAmount &&
                   incomes[0].PaymentDate == salePayment.Date &&
                   incomes[0].AmountReceived == salePayment.InstallmentValue &&
                   incomes[1].PaymentDate == salePayment.Date.AddMonths(1) &&
                   incomes[1].AmountReceived == salePayment.InstallmentValue;
        }
    }
}
