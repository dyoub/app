// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.RentPayments
{
    public class RentPaymentMapping : EntityTypeConfiguration<RentPayment>
    {
        public RentPaymentMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.RentPayments)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.PaymentMethod)
                .WithMany(p => p.RentPayments)
                .HasForeignKey(p => new { p.PaymentMethodId, p.TenantId });

            HasRequired(p => p.RentContract)
                .WithMany(p => p.RentPayments)
                .HasForeignKey(p => new { p.RentContractId, p.TenantId });
        }
    }
}
