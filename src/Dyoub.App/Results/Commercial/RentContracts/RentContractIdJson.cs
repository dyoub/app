// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentContracts
{
    public class RentContractIdJson : JsonResult
    {
        public RentContract RentContract { get; private set; }

        public RentContractIdJson(RentContract rentContract)
        {
            RentContract = rentContract;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                rentContractId = RentContract.Id
            };

            base.ExecuteResult(context);
        }
    }
}
