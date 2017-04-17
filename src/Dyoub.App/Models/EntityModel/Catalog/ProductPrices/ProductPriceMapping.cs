// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Catalog.ProductPrices
{
    public class ProductPriceMapping : EntityTypeConfiguration<ProductPrice>
    {
        public ProductPriceMapping()
        {
            HasKey(p => new
            {
                p.StoreId,
                p.ProductId,
                p.TenantId
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasRequired(p => p.Product)
                .WithMany(p => p.ProductPrices)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });
        }
    }
}
