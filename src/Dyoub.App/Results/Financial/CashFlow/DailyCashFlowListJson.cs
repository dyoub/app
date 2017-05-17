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
                rentContracts = CashFlowAnalysis.Credits.TotalRentContracts(date).Amount,
                sales = CashFlowAnalysis.Credits.TotalSales(date).Amount,
                purchases = CashFlowAnalysis.Debits.TotalPurchases(date).Amount,
                fixedExpenses = CashFlowAnalysis.Debits.TotalFixedExpenses(date).Amount,
                otherCredits = CashFlowAnalysis.Credits.TotalOthers(date).Amount,
                otherDebits = CashFlowAnalysis.Debits.TotalOthers(date).Amount,
                balance = CashFlowAnalysis.Balance(date).Amount
            });

            base.ExecuteResult(context);
        }
    }
}
