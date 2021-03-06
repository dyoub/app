﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Manage.Stores
{
    public class Store : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<FixedExpense> FixedExpenses { get; set; }
        public virtual ICollection<OtherCashActivity> OtherCashActivities { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<ProductStockMovement> ProductStockMovements { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<RentContract> RentContracts { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<ServicePrice> ServicePrices { get; set; }
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
    }
}
