// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Financial.RentIncomes
{
    public class RentIncomeMapping : EntityTypeConfiguration<RentIncome>
    {
        public RentIncomeMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.RentIncomes)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.RentPayment)
                .WithMany(p => p.RentIncomes)
                .HasForeignKey(p => new { p.RentPaymentId, p.TenantId });
        }
    }
}
