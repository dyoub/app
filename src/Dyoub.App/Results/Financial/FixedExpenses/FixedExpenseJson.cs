// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.FixedExpenses
{
    public class FixedExpenseJson : JsonResult
    {
        public FixedExpense FixedExpense { get; private set; }

        public FixedExpenseJson(FixedExpense fixedExpense)
        {
            FixedExpense = fixedExpense;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = FixedExpense == null ? null : new
            {
                id = FixedExpense.Id,
                store = new
                {
                    id = FixedExpense.Store.Id,
                    name = FixedExpense.Store.Name
                },
                description = FixedExpense.Description,
                startDate = FixedExpense.StartDate.ToJson(),
                endDate = FixedExpense.EndDate.ToJson(),
                value = FixedExpense.Value
            };

            base.ExecuteResult(context);
        }
    }
}
