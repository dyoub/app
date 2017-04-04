// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class TenantMapping : EntityTypeConfiguration<Tenant>
    {
        public TenantMapping()
        {
            HasKey(p => p.Id);

            HasMany(p => p.Stores)
                .WithRequired(p => p.Tenant)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.Teams)
                .WithRequired(p => p.Tenant)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.TeamMembers)
                .WithRequired(p => p.Tenant)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.TeamRules)
                .WithRequired(p => p.Tenant)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.Users)
                .WithRequired(p => p.Tenant)
                .HasForeignKey(p => p.TenantId);
        }
    }
}
