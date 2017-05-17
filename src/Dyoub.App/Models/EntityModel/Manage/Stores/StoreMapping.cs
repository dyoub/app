// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Manage.Stores
{
    public class StoreMapping : EntityTypeConfiguration<Store>
    {
        public StoreMapping()
        {
            HasKey(p => new { p.Id, p.TenantId });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Stores)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.FixedExpenses)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.OtherCashActivities)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.ProductPrices)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.ProductStockMovements)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.PurchaseOrders)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.RentContracts)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.SaleOrders)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.ServicePrices)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasMany(p => p.TeamMembers)
                .WithRequired(p => p.Store)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });
        }
    }
}
