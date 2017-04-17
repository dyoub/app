// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Catalog.ServicePrices
{
    public class ServicePriceMapping : EntityTypeConfiguration<ServicePrice>
    {
        public ServicePriceMapping()
        {
            HasKey(p => new
            {
                p.StoreId,
                p.ServiceId,
                p.TenantId
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.ServicePrices)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.ServicePrices)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasRequired(p => p.Service)
                .WithMany(p => p.ServicePrices)
                .HasForeignKey(p => new { p.ServiceId, p.TenantId });
        }
    }
}
