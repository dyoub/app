// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleServices
{
    public class SaleServiceMapping : EntityTypeConfiguration<SaleService>
    {
        public SaleServiceMapping()
        {
            HasKey(p => new
            {
                p.SaleOrderId,
                p.ServiceId,
                p.TenantId
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.SaleServices)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.SaleOrder)
                .WithMany(p => p.SaleServices)
                .HasForeignKey(p => new { p.SaleOrderId, p.TenantId });

            HasRequired(p => p.Service)
                .WithMany(p => p.SaleServices)
                .HasForeignKey(p => new { p.ServiceId, p.TenantId });
        }
    }
}
