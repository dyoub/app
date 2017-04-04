// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Account
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            HasKey(p => new { p.Id, p.TenantId });

            HasOptional(p => p.TeamMember)
                .WithRequired(p => p.User);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.TenantId);
        }
    }
}
