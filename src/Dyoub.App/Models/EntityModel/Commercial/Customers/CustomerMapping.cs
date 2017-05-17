// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.Customers
{
    public class CustomerMapping : EntityTypeConfiguration<Customer>
    {
        public CustomerMapping()
        {
            HasKey(p => new
            {
                p.Id,
                p.TenantId
            });

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(p => p.Tenant)
                .WithMany(p => p.Customers)
                .HasForeignKey(p => p.TenantId);

            HasMany(p => p.RentContracts)
                .WithOptional(p => p.Customer)
                .HasForeignKey(p => new { p.CustomerId, p.TenantId });

            HasMany(p => p.SaleOrders)
                .WithOptional(p => p.Customer)
                .HasForeignKey(p => new { p.CustomerId, p.TenantId });
        }
    }
}
