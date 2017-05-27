// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity.ModelConfiguration;

namespace Dyoub.App.Models.EntityModel.Commercial.RentContractPartners
{
    public class RentContractPartnerMapping : EntityTypeConfiguration<RentContractPartner>
    {
        public RentContractPartnerMapping()
        {
            HasKey(p => new
            {
                p.RentContractId,
                p.PartnerId,
                p.TenantId
            });
            
            HasRequired(p => p.Tenant)
                .WithMany(p => p.RentContractPartners)
                .HasForeignKey(p => p.TenantId);

            HasRequired(p => p.RentContract)
                .WithMany(p => p.RentContractPartners)
                .HasForeignKey(p => new { p.RentContractId, p.TenantId });

            HasRequired(p => p.Partner)
                .WithMany(p => p.RentContractPartners)
                .HasForeignKey(p => new { p.PartnerId, p.TenantId });
        }
    }
}
