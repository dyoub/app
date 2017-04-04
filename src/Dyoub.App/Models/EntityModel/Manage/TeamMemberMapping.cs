// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Manage
{
    public class TeamMemberMapping : EntityTypeConfiguration<TeamMember>
    {
        public TeamMemberMapping()
        {
            HasKey(p => new { p.UserId, p.TenantId });

            HasRequired(p => p.User)
                .WithOptional(p => p.TeamMember);

            HasRequired(p => p.Team)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(p => new { p.TeamId, p.TenantId });

            HasRequired(p => p.Store)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });

            HasRequired(p => p.Tenant)
                .WithMany(p => p.TeamMembers)
                .HasForeignKey(p => p.TenantId);
        }
    }
}
