// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Manage.TeamRules
{
    public class TeamRuleMapping : EntityTypeConfiguration<TeamRule>
    {
        public TeamRuleMapping()
        {
            HasKey(p => new { p.TeamId, p.TenantId, p.Scope });

            HasRequired(p => p.Team)
                .WithMany(p => p.TeamRules)
                .HasForeignKey(p => new { p.TeamId, p.TenantId });

            HasRequired(p => p.Tenant)
                .WithMany(p => p.TeamRules)
                .HasForeignKey(p => p.TenantId);
        }
    }
}
