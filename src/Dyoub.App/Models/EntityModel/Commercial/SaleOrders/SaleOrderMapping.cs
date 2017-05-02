// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleOrders
{
    public class SaleOrderMapping : EntityTypeConfiguration<SaleOrder>
    {
        public SaleOrderMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.SaleOrders)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.SaleOrders)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasOptional(p => p.Customer)
                .WithMany(p => p.SaleOrders)
                .HasForeignKey(p => new { p.CustomerId, p.TenantId });

            HasOptional(p => p.Wallet)
                .WithMany(p => p.SaleOrders)
                .HasForeignKey(p => new { p.WalletId, p.TenantId });

            HasMany(p => p.SalePayments)
                .WithRequired(p => p.SaleOrder)
                .HasForeignKey(p => new { p.SaleOrderId, p.TenantId });

            HasMany(p => p.SaleProducts)
                .WithRequired(p => p.SaleOrder)
                .HasForeignKey(p => new { p.SaleOrderId, p.TenantId });

            HasMany(p => p.SaleServices)
                .WithRequired(p => p.SaleOrder)
                .HasForeignKey(p => new { p.SaleOrderId, p.TenantId });
        }
    }
}
