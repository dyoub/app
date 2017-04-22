// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.OtherCashActivities
{
    public class OtherCashActivityMapping : EntityTypeConfiguration<OtherCashActivity>
    {
        public OtherCashActivityMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.OtherCashActivities)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.OtherCashActivities)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });
        }
    }
}
