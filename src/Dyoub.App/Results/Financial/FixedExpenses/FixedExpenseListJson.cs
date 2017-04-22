// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.FixedExpenses
{
    public class FixedExpenseListJson : JsonResult
    {
        public IEnumerable<FixedExpense> FixedExpenses { get; private set; }

        public FixedExpenseListJson(IEnumerable<FixedExpense> fixedExpenses)
        {
            FixedExpenses = fixedExpenses;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = FixedExpenses.Select(fixedExpense => new
            {
                id = fixedExpense.Id,
                description = fixedExpense.Description,
                startDate = fixedExpense.StartDate,
                endDate = fixedExpense.EndDate,
                value = fixedExpense.Value
            });

            base.ExecuteResult(context);
        }
    }
}
