// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchasePayments
{
    public class PurchasePaymentMapping : EntityTypeConfiguration<PurchasePayment>
    {
        public PurchasePaymentMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.PurchasePayments)
                .HasForeignKey(p => p.TenantId);
            
            HasRequired(p => p.PurchaseOrder)
                .WithMany(p => p.PurchasePayments)
                .HasForeignKey(p => new { p.PurchaseOrderId, p.TenantId });
        }
    }
}
