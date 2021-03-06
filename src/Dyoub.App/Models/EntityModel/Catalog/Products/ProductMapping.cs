﻿// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Catalog.Products
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            HasKey(p => new { p.Id, p.TenantId });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.ProductPrices)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });

            HasMany(p => p.ProductStockMovements)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });

            HasMany(p => p.PurchasedProducts)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });

            HasMany(p => p.RentedProducts)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });

            HasMany(p => p.SaleProducts)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });
        }
    }
}
