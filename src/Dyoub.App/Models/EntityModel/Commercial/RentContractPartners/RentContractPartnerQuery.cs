// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.RentContractPartners
{
    public static class RentContractPartnerQuery
    {
        public static IQueryable<RentContractPartner> IncludePartner(this IQueryable<RentContractPartner> rentContractPartners)
        {
            return rentContractPartners.Include(rentContractPartner => rentContractPartner.Partner);
        }

        public static IQueryable<RentContractPartner> IncludeRentContract(this IQueryable<RentContractPartner> rentContractPartners)
        {
            return rentContractPartners.Include(rentContractPartner => rentContractPartner.RentContract);
        }

        public static IQueryable<RentContractPartner> WherePartnerId(this IQueryable<RentContractPartner> rentContractPartners, int partnerId)
        {
            return rentContractPartners.Where(rentContractPartner => rentContractPartner.PartnerId == partnerId);
        }

        public static IQueryable<RentContractPartner> WhereRentContractId(this IQueryable<RentContractPartner> rentContractPartners, int rentContractId)
        {
            return rentContractPartners.Where(rentContractPartner => rentContractPartner.RentContractId == rentContractId);
        }
    }
}
