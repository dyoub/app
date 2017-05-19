// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.RentContracts
{
    public class RentContractMapping : EntityTypeConfiguration<RentContract>
    {
        public RentContractMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.RentContracts)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.RentContracts)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasOptional(p => p.Customer)
                .WithMany(p => p.RentContracts)
                .HasForeignKey(p => new { p.CustomerId, p.TenantId });

            HasOptional(p => p.Wallet)
                .WithMany(p => p.RentContracts)
                .HasForeignKey(p => new { p.WalletId, p.TenantId });
            
            HasMany(p => p.RentedProducts)
                .WithRequired(p => p.RentContract)
                .HasForeignKey(p => new { p.RentContractId, p.TenantId });

            HasMany(p => p.RentPayments)
                .WithRequired(p => p.RentContract)
                .HasForeignKey(p => new { p.RentContractId, p.TenantId });
        }
    }
}
