// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.Wallets
{
    public class WalletMapping : EntityTypeConfiguration<Wallet>
    {
        public WalletMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Wallets)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.SaleOrders)
                .WithOptional(p => p.Wallet)
                .HasForeignKey(p => new { p.WalletId, p.TenantId });
        }
    }
}
