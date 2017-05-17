// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.ServiceModel.Financial;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Financial.CashFlow
{
    public class DailyCashFlowListJson : JsonResult
    {
        public CashFlowAnalysis CashFlowAnalysis { get; private set; }

        public DailyCashFlowListJson(CashFlowAnalysis cashFlowAnalysis)
        {
            CashFlowAnalysis = cashFlowAnalysis;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = CashFlowAnalysis.DailyPeriod.Select(date => new
            {
                date = date.ToJson(),
                sales = CashFlowAnalysis.TotalSales(date).Amount,
                purchases = CashFlowAnalysis.TotalPurchases(date).Amount,
                fixedExpenses = CashFlowAnalysis.TotalFixedExpenses(date).Amount,
                otherCredits = CashFlowAnalysis.TotalOtherCredits(date).Amount,
                otherDebits = CashFlowAnalysis.TotalOtherDebits(date).Amount,
                balance = CashFlowAnalysis.Balance(date).Amount
            });

            base.ExecuteResult(context);
        }
    }
}
