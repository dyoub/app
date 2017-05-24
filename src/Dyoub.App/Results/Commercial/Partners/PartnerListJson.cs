// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Partners;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.Partners
{
    public class PartnerListJson : JsonResult
    {
        public IEnumerable<Partner> Partners { get; private set; }

        public PartnerListJson(IEnumerable<Partner> partners)
        {
            Partners = partners;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Partners.Select(partner => new
            {
                id = partner.Id,
                name = partner.Name,
                nationalId = partner.NationalId,
                email = partner.Email,
                phoneNumber = partner.PhoneNumber,
                alternativePhoneNumber = partner.AlternativePhoneNumber
            });

            base.ExecuteResult(context);
        }
    }
}
