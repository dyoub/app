// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleProducts
{
    public class SaleProductMapping : EntityTypeConfiguration<SaleProduct>
    {
        public SaleProductMapping()
        {
            HasKey(p => new
            {
                p.SaleOrderId,
                p.ProductId,
                p.TenantId
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.SaleProducts)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.SaleOrder)
                .WithMany(p => p.SaleProducts)
                .HasForeignKey(p => new { p.SaleOrderId, p.TenantId });

            HasRequired(p => p.Product)
                .WithMany(p => p.SaleProducts)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });
        }
    }
}
