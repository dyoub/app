// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts
{
    public class PurchasedProduct : ITenantData
    {
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalCost { get; set; }
        public Guid? StockTransactionId { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tenant Tenant { get; set; }
    }
}
