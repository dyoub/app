// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.PaymentMethods
{
    public class PaymentMethodMapping : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.PaymentMethods)
                .HasForeignKey(p => p.TenantId);
            
            HasMany(p => p.PaymentMethodFees)
                .WithRequired(p => p.PaymentMethod)
                .HasForeignKey(p => new { p.PaymentMethodId, p.TenantId })
                .WillCascadeOnDelete();

            HasMany(p => p.SalePayments)
                .WithRequired(p => p.PaymentMethod)
                .HasForeignKey(p => new { p.PaymentMethodId, p.TenantId });
        }
    }
}
