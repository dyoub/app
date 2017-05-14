// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Manage.Stores;

namespace Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements
{
    public class ProductStockMovement : ITenantData
    {
        public Guid TransactionId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
