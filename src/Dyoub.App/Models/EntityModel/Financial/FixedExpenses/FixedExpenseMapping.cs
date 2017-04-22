// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.FixedExpenses
{
    public class FixedExpenseMapping : EntityTypeConfiguration<FixedExpense>
    {
        public FixedExpenseMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.FixedExpenses)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.Store)
                .WithMany(p => p.FixedExpenses)
                .HasForeignKey(p => new { p.StoreId, p.TenantId });
        }
    }
}
