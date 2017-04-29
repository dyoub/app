// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;

namespace Dyoub.App.Models.EntityModel.Catalog.ItemPrices
{
    public class ItemPrice : ITenantData
    {
        public int ItemId { get { return (ProductId ?? ServiceId).Value; } }
        public int? ProductId { get; set; }
        public int? ServiceId { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Marketed { get; set; }
        public bool CanFraction { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool IsProduct { get { return ProductId != null; } }
        public bool IsService { get { return ServiceId != null; } }
        public bool HasPrice { get { return UnitPrice != null; } }

        public ProductPrice ToProductPrice()
        {
            return new ProductPrice
            {
                StoreId = StoreId,
                ProductId = ProductId.Value,
                UnitPrice = UnitPrice.Value
            };
        }

        public ServicePrice ToServicePrice()
        {
            return new ServicePrice
            {
                StoreId = StoreId,
                ServiceId = ServiceId.Value,
                UnitPrice = UnitPrice.Value
            };
        }
    }
}
