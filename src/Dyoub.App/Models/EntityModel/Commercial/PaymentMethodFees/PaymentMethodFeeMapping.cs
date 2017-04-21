// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees
{
    public class PaymentMethodFeeMapping : EntityTypeConfiguration<PaymentMethodFee>
    {
        public PaymentMethodFeeMapping()
        {
            HasKey(p => new
            {
                p.PaymentMethodId,
                p.TenantId,
                p.MinimumInstallment
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.PaymentMethodFees)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.PaymentMethod)
                .WithMany(p => p.PaymentMethodFees)
                .HasForeignKey(p => new { p.PaymentMethodId, p.TenantId });
        }
    }
}
