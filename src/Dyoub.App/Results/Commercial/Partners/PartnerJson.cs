// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Partners;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.Partners
{
    public class PartnerJson : JsonResult
    {
        public Partner Partner { get; private set; }

        public PartnerJson(Partner partner)
        {
            Partner = partner;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Partner != null)
            {
                Data = new
                {
                    id = Partner.Id,
                    name = Partner.Name,
                    nationalId = Partner.NationalId,
                    email = Partner.Email,
                    phoneNumber = Partner.PhoneNumber,
                    alternativePhoneNumber = Partner.AlternativePhoneNumber,
                    additionalInformation = Partner.AdditionalInformation
                };
            }

            base.ExecuteResult(context);
        }
    }
}
