// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders
{
    public class PurchaseOrderMapping : EntityTypeConfiguration<PurchaseOrder>
    {
        public PurchaseOrderMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasOptional(p => p.Supplier)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => new { p.SupplierId, p.TenantId });

            HasOptional(p => p.Wallet)
                .WithMany(p => p.PurchaseOrders)
                .HasForeignKey(p => new { p.WalletId, p.TenantId });

            HasMany(p => p.PurchasedProducts)
                .WithRequired(p => p.PurchaseOrder)
                .HasForeignKey(p => new { p.PurchaseOrderId, p.TenantId });
        }
    }
}
