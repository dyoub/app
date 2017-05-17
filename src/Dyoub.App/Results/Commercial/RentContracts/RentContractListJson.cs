// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentContracts
{
    public class RentContractListJson : JsonResult
    {
        public IEnumerable<RentContract> RentContracts { get; private set; }

        public IEnumerable<IGrouping<DateTime, RentContract>> RentContractHistory
        {
            get
            {
                return RentContracts.GroupBy(rentContract => rentContract.StartDate);
            }
        }

        public RentContractListJson(IEnumerable<RentContract> rentContracts)
        {
            RentContracts = rentContracts;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContractHistory.Select(history => new
            {
                date = history.Key,
                rentContractList = history.Select(rentContract => new
                {
                    id = rentContract.Id,
                    startDate = rentContract.StartDate.ToJson(),
                    store = rentContract.Store.Name,
                    customer = rentContract.Customer != null ? rentContract.Customer.Name : null,
                    draft = rentContract.Draft,
                    budget = rentContract.Budget
                })
            });

            base.ExecuteResult(context);
        }
    }
}
