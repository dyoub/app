// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.Commercial.SaleOrders
{
    public class DeleteConfirmedSaleOrderContext : TenantContext
    {
        public SaleOrder SaleOrder { get; private set; }

        public DeleteConfirmedSaleOrderContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));

            SaleOrder = SaleOrders.Add(SaleOrderFactory.ConfirmedSaleOrder(store));

            SaveChanges();
        }

        public bool SaleOrderWasNotDeleted()
        {
            return SaleOrders.WhereId(SaleOrder.Id).Any();
        }
    }
}
