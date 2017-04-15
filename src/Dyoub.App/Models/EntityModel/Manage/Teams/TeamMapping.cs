// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Manage.Teams
{
    public class TeamMapping : EntityTypeConfiguration<Team>
    {
        public TeamMapping()
        {
            HasKey(p => new { p.Id, p.TenantId });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Teams)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.TeamMembers)
                .WithRequired(p => p.Team)
                .HasForeignKey(p => new { p.TeamId, p.TenantId });

            HasMany(p => p.TeamRules)
                .WithRequired(p => p.Team)
                .HasForeignKey(p => new { p.TeamId, p.TenantId });
        }
    }
}
