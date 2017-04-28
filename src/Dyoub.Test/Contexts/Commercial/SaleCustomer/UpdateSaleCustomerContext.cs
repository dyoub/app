// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.SaleCustomer
{
    public class UpdateSaleCustomerContext : TenantContext
    {
        private SaleOrder original;

        public SaleOrder SaleOrder { get; private set; }
        public Customer AnotherCustomer { get; private set; }

        public UpdateSaleCustomerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Customer customer = Customers.Add(CustomerFactory.Customer(tenant));
            Store store = Stores.Add(StoreFactory.Store(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store, customer));
            AnotherCustomer = Customers.Add(CustomerFactory.Customer(tenant));

            original = SaleOrderFactory.SaleOrder(store, customer);

            SaveChanges();
        }

        public bool SaleOrderWasUpdated()
        {
            Entry(SaleOrder).Reload();
            return SaleOrder.Customer.Id == AnotherCustomer.Id;
        }
    }
}
