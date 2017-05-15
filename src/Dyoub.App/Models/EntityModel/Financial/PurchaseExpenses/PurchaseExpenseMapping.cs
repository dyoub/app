// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses
{
    public class PurchaseExpenseMapping : EntityTypeConfiguration<PurchaseExpense>
    {
        public PurchaseExpenseMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.PurchaseExpenses)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.PurchasePayment)
                .WithMany(p => p.PurchaseExpenses)
                .HasForeignKey(p => new { p.PurchasePaymentId, p.TenantId });
        }
    }
}
