// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.ServiceModel.Financial;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class FinancialOverviewJson : JsonResult
    {
        public CashFlowAnalysis CashFlowAnalysis { get; private set; }

        public FinancialOverviewJson(CashFlowAnalysis cashFlowAnalysis)
        {
            CashFlowAnalysis = cashFlowAnalysis;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    fixedExpenses = CashFlowAnalysis.TotalFixedExpenses().Amount
                }
            };

            base.ExecuteResult(context);
        }
    }
}
