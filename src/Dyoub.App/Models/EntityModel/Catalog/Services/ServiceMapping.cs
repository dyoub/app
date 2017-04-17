// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Catalog.Services
{
    public class ServiceMapping : EntityTypeConfiguration<Service>
    {
        public ServiceMapping()
        {
            HasKey(p => new { p.Id, p.TenantId });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Services)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.ServicePrices)
                .WithRequired(p => p.Service)
                .HasForeignKey(p => new { p.ServiceId, p.TenantId });
        }
    }
}
