﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.Commercial.SaleOrders
{
    public class UpdateConfirmedSaleOrderContext : TenantContext
    {
        private SaleOrder original;

        public Store AnotherStore { get; private set; }
        public SaleOrder SaleOrder { get; private set; }

        public UpdateConfirmedSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            AnotherStore = Stores.Add(StoreFactory.Store(tenant));
            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));

            original = SaleOrderFactory.SaleOrder(store);

            SaveChanges();
        }

        public bool SaleOrderWasNotChanged()
        {
            Entry(SaleOrder).Reload();

            return SaleOrder.Store.Id == original.Store.Id &&
                   SaleOrder.IssueDate == original.IssueDate &&
                   SaleOrder.AdditionalInformation == original.AdditionalInformation;
        }
    }
}
