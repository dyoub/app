// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.RentedProducts
{
    public class RentedProductMapping : EntityTypeConfiguration<RentedProduct>
    {
        public RentedProductMapping()
        {
            HasKey(p => new
            {
                p.RentContractId,
                p.ProductId,
                p.TenantId
            });

            HasRequired(p => p.Tenant)
                .WithMany(p => p.RentedProducts)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.RentContract)
                .WithMany(p => p.RentedProducts)
                .HasForeignKey(p => new { p.RentContractId, p.TenantId });

            HasRequired(p => p.Product)
                .WithMany(p => p.RentedProducts)
                .HasForeignKey(p => new { p.ProductId, p.TenantId });
        }
    }
}
