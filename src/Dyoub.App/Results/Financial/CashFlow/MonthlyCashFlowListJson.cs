// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.ServiceModel.Financial;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.CashFlow
{
    public class MonthlyCashFlowListJson : JsonResult
    {
        public CashFlowAnalysis CashFlowAnalysis { get; private set; }

        public MonthlyCashFlowListJson(CashFlowAnalysis cashFlowAnalysis)
        {
            CashFlowAnalysis = cashFlowAnalysis;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = CashFlowAnalysis.MonthlyPeriod.Select(baseDate => new
            {
                baseDate = baseDate,
                sales = CashFlowAnalysis.TotalSales(baseDate.Month, baseDate.Year).Amount,
                purchases = CashFlowAnalysis.TotalPurchases(baseDate.Month, baseDate.Year).Amount,
                fixedExpenses = CashFlowAnalysis.TotalFixedExpenses(baseDate.Month, baseDate.Year).Amount,
                otherCredits = CashFlowAnalysis.TotalOtherCredits(baseDate.Month, baseDate.Year).Amount,
                otherDebits = CashFlowAnalysis.TotalOtherDebits(baseDate.Month, baseDate.Year).Amount,
                balance = CashFlowAnalysis.Balance(baseDate.Month, baseDate.Year).Amount
            });

            base.ExecuteResult(context);
        }
    }
}
