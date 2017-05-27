// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentContracts
{
    public class RentContractJson : JsonResult
    {
        public RentContract RentContract { get; private set; }

        public RentContractJson(RentContract rentContract)
        {
            RentContract = rentContract;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                startDate = RentContract.StartDate.Date.ToJson(),
                startTime = RentContract.StartDate.TimeOfDay.ToJson(),
                endDate = RentContract.EndDate.Date.ToJson(),
                endTime = RentContract.EndDate.TimeOfDay.ToJson(),
                confirmationDate = RentContract.ConfirmationDate.ToJsonLocalTimeZone(),
                location = RentContract.Location,
                additionalInformation = RentContract.AdditionalInformation,
                total = RentContract.Total,
                billedAmount = RentContract.BilledAmount,
                confirmed = RentContract.Confirmed,
                returnPending = RentContract.ReturnPending,
                store = new
                {
                    id = RentContract.Store.Id,
                    name = RentContract.Store.Name
                },
                wallet = RentContract.Wallet == null ? null : new
                {
                    id = RentContract.Wallet.Id,
                    name = RentContract.Wallet.Name
                }
            };

            base.ExecuteResult(context);
        }
    }
}
