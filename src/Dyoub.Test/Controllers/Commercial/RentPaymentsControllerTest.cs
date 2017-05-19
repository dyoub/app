// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Models.ViewModel.Commercial.RentPayments;
using Dyoub.App.Results.Commercial.RentPayments;
using Dyoub.App.Results.Common;
using Dyoub.Test.Contexts.Commercial.RentPayments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class RentPaymentsControllerTest
    {
        [TestMethod]
        public async Task ListRentPayments()
        {
            ListRentPaymentsContext context = new ListRentPaymentsContext();
            RentPaymentsController controller = new RentPaymentsController(context);

            RentContractIdViewModel viewModel = new RentContractIdViewModel();
            viewModel.Id = context.RentContract.Id;

            RentPaymentListJson result = (RentPaymentListJson)await controller.List(viewModel);

            Assert.IsTrue(result.RentContract.RentPayments.Count() == context.RentPayments.Count());
        }

        [TestMethod]
        public async Task UpdateRentPayments()
        {
            UpdateRentPaymentsContext context = new UpdateRentPaymentsContext();
            RentPaymentsController controller = new RentPaymentsController(context);

            UpdateRentPaymentsViewModel viewModel = new UpdateRentPaymentsViewModel();
            viewModel.RentContractId = context.RentPayment.RentContract.Id;
            viewModel.Discount = context.RentPayment.RentContract.Discount;
            viewModel.Payments.Add(new RentPaymentViewModel
            {
                Date = context.RentPayment.Date.AddDays(1),
                NumberOfInstallments = context.RentPayment.NumberOfInstallments + 1,
                PaymentMethodId = context.AnotherPaymentMethod.Id,
                Total = context.RentPayment.RentContract.TotalPayable
            });

            await controller.Update(viewModel);

            Assert.IsTrue(context.RentPaymentsWasUpdated());
        }

        [TestMethod]
        public async Task UpdatePaymentsOfConfirmedRentContract()
        {
            UpdatePaymentsOfConfirmedRentContractContext context = new UpdatePaymentsOfConfirmedRentContractContext();
            RentPaymentsController controller = new RentPaymentsController(context);

            UpdateRentPaymentsViewModel viewModel = new UpdateRentPaymentsViewModel();
            viewModel.RentContractId = context.RentPayment.RentContract.Id;
            viewModel.Discount = context.RentPayment.RentContract.Discount;
            viewModel.Payments.Add(new RentPaymentViewModel
            {
                Date = context.RentPayment.Date.AddDays(1),
                NumberOfInstallments = context.RentPayment.NumberOfInstallments + 1,
                PaymentMethodId = context.AnotherPaymentMethod.Id,
                Total = context.RentPayment.RentContract.TotalPayable
            });

            await controller.Update(viewModel);

            ActionResult result = await controller.Update(viewModel);

            Assert.IsTrue(context.RentPaymentsNotChanged());
            Assert.IsTrue(result is ModelErrorsJson);
        }
    }
}
