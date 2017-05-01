// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Models.ViewModel.Commercial.SalePayments;
using Dyoub.App.Results.Commercial.SalePayments;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.SalePayments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class SalePaymentsControllerTest
    {
        [TestMethod]
        public async Task ListSalePayments()
        {
            ListSalePaymentsContext context = new ListSalePaymentsContext();
            SalePaymentsController controller = new SalePaymentsController(context);

            SaleOrderIdViewModel viewModel = new SaleOrderIdViewModel();
            viewModel.Id = context.SaleOrder.Id;

            SalePaymentListJson result = (SalePaymentListJson)await controller.List(viewModel);

            Assert.IsTrue(result.SaleOrder.SalePayments.Count() == context.SalePayments.Count());
        }

        [TestMethod]
        public async Task UpdateSalePayments()
        {
            UpdateSalePaymentsContext context = new UpdateSalePaymentsContext();
            SalePaymentsController controller = new SalePaymentsController(context);
            
            UpdateSalePaymentsViewModel viewModel = new UpdateSalePaymentsViewModel();
            viewModel.SaleOrderId = context.SalePayment.SaleOrder.Id;
            viewModel.Discount = context.SalePayment.SaleOrder.Discount;
            viewModel.Payments.Add(new SalePaymentViewModel
            {
                Date = context.SalePayment.Date.AddDays(1),
                NumberOfInstallments = context.SalePayment.NumberOfInstallments + 1,
                PaymentMethodId = context.AnotherPaymentMethod.Id,
                Total = context.SalePayment.SaleOrder.TotalPayable
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.SalePaymentsWasUpdated());
        }

        [TestMethod]
        public async Task UpdatePaymentsOfConfirmedSaleOrder()
        {
            UpdatePaymentsOfConfirmedSaleOrderContext context = new UpdatePaymentsOfConfirmedSaleOrderContext();
            SalePaymentsController controller = new SalePaymentsController(context);

            UpdateSalePaymentsViewModel viewModel = new UpdateSalePaymentsViewModel();
            viewModel.SaleOrderId = context.SalePayment.SaleOrder.Id;
            viewModel.Discount = context.SalePayment.SaleOrder.Discount;
            viewModel.Payments.Add(new SalePaymentViewModel
            {
                Date = context.SalePayment.Date.AddDays(1),
                NumberOfInstallments = context.SalePayment.NumberOfInstallments + 1,
                PaymentMethodId = context.AnotherPaymentMethod.Id,
                Total = context.SalePayment.SaleOrder.TotalPayable
            });

            await controller.Update(viewModel);

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.SalePaymentsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
