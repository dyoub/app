// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Financial;
using Dyoub.App.Models.ViewModel.Financial.FixedExpenses;
using Dyoub.App.Results.Financial.FixedExpenses;
using Dyoub.Test.Contexts.Financial.FixedExpenses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Financial
{
    [TestClass]
    public class FixedExpensesControllerTest
    {
        [TestMethod]
        public async Task CreateFixedExpense()
        {
            CreateFixedExpenseContext context = new CreateFixedExpenseContext();
            FixedExpensesController controller = new FixedExpensesController(context);

            CreateFixedExpenseViewModel viewModel = new CreateFixedExpenseViewModel();
            viewModel.StoreId = context.Store.Id;
            viewModel.Description = "Test";
            viewModel.StartDate = DateTime.Now;
            viewModel.Value = 10.50M;

            await controller.Create(viewModel);

            Assert.IsTrue(context.FixedExpenseWasCreated());
        }

        [TestMethod]
        public async Task DeleteFixedExpense()
        {
            DeleteFixedExpenseContext context = new DeleteFixedExpenseContext();
            FixedExpensesController controller = new FixedExpensesController(context);

            FixedExpenseIdViewModel viewModel = new FixedExpenseIdViewModel();
            viewModel.Id = context.FixedExpense.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.FixedExpenseWasDeleted());
        }

        [TestMethod]
        public async Task FindFixedExpense()
        {
            FindFixedExpenseContext context = new FindFixedExpenseContext();
            FixedExpensesController controller = new FixedExpensesController(context);

            FixedExpenseIdViewModel viewModel = new FixedExpenseIdViewModel();
            viewModel.Id = context.FixedExpense.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is FixedExpenseJson);
        }

        [TestMethod]
        public async Task ListFixedExpense()
        {
            ListFixedExpenseContext context = new ListFixedExpenseContext();
            FixedExpensesController controller = new FixedExpensesController(context);

            FixedExpenseListJson result = (FixedExpenseListJson)await controller.List(new ListFixedExpensesViewModel());

            Assert.IsTrue(result.FixedExpenses.Count() == context.FixedExpenses.Count());
        }

        [TestMethod]
        public async Task UpdateFixedExpense()
        {
            UpdateFixedExpenseContext context = new UpdateFixedExpenseContext();
            FixedExpensesController controller = new FixedExpensesController(context);

            UpdateFixedExpenseViewModel viewModel = new UpdateFixedExpenseViewModel();
            viewModel.Id = context.FixedExpense.Id;
            viewModel.StoreId = context.AnotherStore.Id;
            viewModel.Description = context.FixedExpense.Description + " Updated";
            viewModel.StartDate = context.FixedExpense.StartDate.AddDays(1);
            viewModel.EndDate = context.FixedExpense.EndDate.Value.AddDays(1);
            viewModel.Value = context.FixedExpense.Value + 10;

            await controller.Update(viewModel);

            Assert.IsTrue(context.FixedExpenseWasUpdated());
        }
    }
}
