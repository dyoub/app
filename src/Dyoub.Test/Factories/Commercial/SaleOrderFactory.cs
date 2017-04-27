// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Commercial
{
    public class SaleOrderFactory
    {
        public static SaleOrder SaleOrder(Store store, Customer customer = null)
        {
            return new SaleOrder
            {
                Store = store,
                Customer = customer,
                Tenant = store.Tenant,
                IssueDate = DateTime.Today,
                AdditionalInformation = "Additional information.",
                Author = "user@email.com",
                CreatedAt = DateTime.Now
            };
        }

        public static SaleOrder ConfirmedSaleOrder(Store store, Customer customer = null)
        {
            SaleOrder saleOrder = SaleOrder(store, customer);
            saleOrder.ConfirmationDate = DateTime.Now;

            return saleOrder;
        }
    }
}
