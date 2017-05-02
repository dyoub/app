// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.SaleIncomes
{
    public class SaleIncomeMapping : EntityTypeConfiguration<SaleIncome>
    {
        public SaleIncomeMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.SaleIncomes)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.SalePayment)
                .WithMany(p => p.SaleIncomes)
                .HasForeignKey(p => new { p.SalePaymentId, p.TenantId });
        }
    }
}
