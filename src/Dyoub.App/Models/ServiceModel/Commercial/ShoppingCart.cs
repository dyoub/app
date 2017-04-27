// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Commercial
{
    public class ShoppingCart
    {
        public TenantContext Tenant { get; private set;}
        public SaleOrder SaleOrder { get; private set; }

        public ShoppingCart(TenantContext tenant)
        {
            Tenant = tenant;
        }
    }
}
