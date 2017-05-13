// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Inventory;
using Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.ViewModel.Inventory.PurchasePayments;
using Dyoub.App.Results.Inventory.PurchasePayments;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Inventory.PurchasePayments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Inventory
{
    [TestClass]
    public class PurchasePaymentsControllerTest
    {
        [TestMethod]
        public async Task ListPurchasePayments()
        {
            ListPurchasePaymentsContext context = new ListPurchasePaymentsContext();
            PurchasePaymentsController controller = new PurchasePaymentsController(context);

            PurchaseOrderIdViewModel viewModel = new PurchaseOrderIdViewModel();
            viewModel.Id = context.PurchaseOrder.Id;

            PurchasePaymentListJson result = (PurchasePaymentListJson)await controller.List(viewModel);

            Assert.IsTrue(result.PurchaseOrder.PurchasePayments.Count() == context.PurchasePayments.Count());
        }

        [TestMethod]
        public async Task UpdatePurchasePayments()
        {
            UpdatePurchasePaymentsContext context = new UpdatePurchasePaymentsContext();
            PurchasePaymentsController controller = new PurchasePaymentsController(context);

            UpdatePurchasePaymentsViewModel viewModel = new UpdatePurchasePaymentsViewModel();
            viewModel.PurchaseOrderId = context.PurchasePayment.PurchaseOrder.Id;
            viewModel.Discount = context.PurchasePayment.PurchaseOrder.Discount;
            viewModel.Payments.Add(new PurchasePaymentViewModel
            {
                Date = context.PurchasePayment.Date.AddDays(1),
                NumberOfInstallments = context.PurchasePayment.NumberOfInstallments + 1,
                Total = context.PurchasePayment.PurchaseOrder.TotalPayable
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.PurchasePaymentsWasUpdated());
        }

        [TestMethod]
        public async Task UpdatePaymentsOfConfirmedPurchaseOrder()
        {
            UpdatePaymentsOfConfirmedPurchaseOrderContext context = new UpdatePaymentsOfConfirmedPurchaseOrderContext();
            PurchasePaymentsController controller = new PurchasePaymentsController(context);

            UpdatePurchasePaymentsViewModel viewModel = new UpdatePurchasePaymentsViewModel();
            viewModel.PurchaseOrderId = context.PurchasePayment.PurchaseOrder.Id;
            viewModel.Discount = context.PurchasePayment.PurchaseOrder.Discount;
            viewModel.Payments.Add(new PurchasePaymentViewModel
            {
                Date = context.PurchasePayment.Date.AddDays(1),
                NumberOfInstallments = context.PurchasePayment.NumberOfInstallments + 1,
                Total = context.PurchasePayment.PurchaseOrder.TotalPayable
            });

            await controller.Update(viewModel);

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.PurchasePaymentsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
