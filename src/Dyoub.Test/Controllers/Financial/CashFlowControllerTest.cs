// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Financial;
using Dyoub.App.Models.ViewModel.Financial.CashFlow;
using Dyoub.App.Results.Financial.CashFlow;
using Dyoub.Test.Contexts.Financial.CashFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Financial
{
    [TestClass]
    public class CashFlowControllerTest
    {
        [TestMethod]
        public async Task DailyCashFlow()
        {
            DailyCashFlowContext context = new DailyCashFlowContext();
            CashFlowController controller = new CashFlowController(context);

            DailyCashFlowViewModel viewModel = new DailyCashFlowViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.Month = DateTime.Today.Month;
            viewModel.Year = DateTime.Today.Year;

            ActionResult result = await controller.Daily(viewModel);

            Assert.IsTrue(result is DailyCashFlowListJson);
        }

        [TestMethod]
        public async Task MonthlyCashFlow()
        {
            MonthlyCashFlowContext context = new MonthlyCashFlowContext();
            CashFlowController controller = new CashFlowController(context);

            MonthlyCashFlowViewModel viewModel = new MonthlyCashFlowViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.Year = DateTime.Today.Year;

            ActionResult result = await controller.Monthly(viewModel);

            Assert.IsTrue(result is MonthlyCashFlowListJson);
        }
    }
}
