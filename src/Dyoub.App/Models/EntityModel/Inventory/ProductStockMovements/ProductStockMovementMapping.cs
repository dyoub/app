// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements
{
    public class ProductStockMovementMapping : EntityTypeConfiguration<ProductStockMovement>
    {
        public ProductStockMovementMapping()
        {
            HasKey(p => new
            {
                p.TransactionId,
                p.ProductId,
                p.StoreId,
                p.TenantId
            });

            HasRequired(p => p.Tenant)
                .WithMany(p => p.ProductStockMovements)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Product)
                .WithMany(p => p.ProductStockMovements)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });

            HasRequired(p => p.Store)
                .WithMany(p => p.ProductStockMovements)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });
        }
    }
}
