// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts
{
    public class PurchasedProductMapping : EntityTypeConfiguration<PurchasedProduct>
    {
        public PurchasedProductMapping()
        {
            HasKey(p => new
            {
                p.PurchaseOrderId,
                p.ProductId,
                p.TenantId
            });

            HasRequired(p => p.Tenant)
                .WithMany(p => p.PurchasedProducts)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.PurchaseOrder)
                .WithMany(p => p.PurchasedProducts)
                .HasForeignKey(p => new { p.PurchaseOrderId, p.TenantId });

            HasRequired(p => p.Product)
                .WithMany(p => p.PurchasedProducts)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });
        }
    }
}
