// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Overview;
using Dyoub.App.Models.ServiceModel.Financial;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Account.Dashboard
{
    public class FinancialOverviewJson : JsonResult
    {
        public FinancialCount Counter { get; private set; }
        public CashFlowAnalysis CashFlowAnalysis { get; private set; }

        public FinancialOverviewJson(FinancialCount counter, CashFlowAnalysis cashFlowAnalysis)
        {
            Counter = counter;
            CashFlowAnalysis = cashFlowAnalysis;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                counter = new
                {
                    cashFlowBalance = CashFlowAnalysis.Balance(DateTime.Today).Amount,
                    fixedExpenses = CashFlowAnalysis.TotalFixedExpenses().Amount,
                    otherCashActivities = CashFlowAnalysis.Others.Count(),
                    wallets = Counter.Wallets
                }
            };

            base.ExecuteResult(context);
        }
    }
}
