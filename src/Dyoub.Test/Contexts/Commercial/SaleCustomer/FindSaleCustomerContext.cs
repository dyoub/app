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
    public class FindSaleCustomerContext : TenantContext
    {
        public SaleOrder SaleOrder { get; private set; }

        public FindSaleCustomerContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Customer customer = Customers.Add(CustomerFactory.Customer(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.SaleOrder(store, customer));

            SaveChanges();
        }
    }
}
