// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.RentContractCustomer
{
    public class UpdateCustomerToConfirmedRentContractContext : TenantContext
    {
        private RentContract original;

        public RentContract RentContract { get; private set; }
        public Customer AnotherCustomer { get; private set; }

        public UpdateCustomerToConfirmedRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Customer customer = Customers.Add(CustomerFactory.Customer(tenant));
            Store store = Stores.Add(StoreFactory.Store(tenant));

            RentContract = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store, customer));
            AnotherCustomer = Customers.Add(CustomerFactory.Customer(tenant));

            original = RentContractFactory.RentContract(store, customer);

            SaveChanges();
        }

        public bool RentContractWasNotChanged()
        {
            Entry(RentContract).Reload();
            return RentContract.Customer.Id == original.Customer.Id;
        }
    }
}
