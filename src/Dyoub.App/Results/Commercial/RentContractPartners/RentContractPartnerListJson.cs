// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentContractPartners
{
    public class RentContractPartnerListJson : JsonResult
    {
        public RentContract RentContract { get; private set; }

        public RentContractPartnerListJson(RentContract rentContract)
        {
            RentContract = rentContract;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                confirmed = RentContract.Confirmed,
                returnPending = RentContract.ReturnPending,
                partnerList = RentContract.RentContractPartners.Select(rentContractPartner => new
                {
                    id = rentContractPartner.Partner.Id,
                    name = rentContractPartner.Partner.Name,
                    email = rentContractPartner.Partner.Email,
                    phoneNumber = rentContractPartner.Partner.PhoneNumber,
                    alternativePhoneNumber = rentContractPartner.Partner.AlternativePhoneNumber,
                    additionalInformation = rentContractPartner.Partner.AdditionalInformation
                })
            };

            base.ExecuteResult(context);
        }
    }
}
