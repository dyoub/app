// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Catalog.Products
{
    public class Product : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Marketed { get; set; }
        public bool IsManufactured { get; set; }
        public bool StockMovement { get; set; }
        public bool CanFraction { get; set; }

        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
        public virtual ICollection<PurchasedProduct> PurchasedProducts { get; set; }
        public virtual ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
