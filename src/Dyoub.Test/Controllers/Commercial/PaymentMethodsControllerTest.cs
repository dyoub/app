// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Controllers.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.PaymentMethods;
using Dyoub.App.Results.Commercial.PaymentMethods;
using Dyoub.Test.Contexts.Commercial.PaymentMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.Test.Controllers.Commercial
{
    [TestClass]
    public class PaymentMethodsControllerTest
    {
        [TestMethod]
        public async Task CreatePaymentMethod()
        {
            CreatePaymentMethodContext context = new CreatePaymentMethodContext();
            PaymentMethodsController controller = new PaymentMethodsController(context);

            CreatePaymentMethodViewModel viewModel = new CreatePaymentMethodViewModel();
            viewModel.Name = "Payment Method Test";
            viewModel.InstallmentLimit = 12;

            await controller.Create(viewModel);

            Assert.IsTrue(context.PaymentMethodWasCreated());
        }

        [TestMethod]
        public async Task DeletePaymentMethod()
        {
            DeletePaymentMethodContext context = new DeletePaymentMethodContext();
            PaymentMethodsController controller = new PaymentMethodsController(context);

            PaymentMethodIdViewModel viewModel = new PaymentMethodIdViewModel();
            viewModel.Id = context.PaymentMethod.Id;

            await controller.Delete(viewModel);

            Assert.IsTrue(context.PaymentMethodWasDeleted());
        }

        [TestMethod]
        public async Task FindPaymentMethod()
        {
            FindPaymentMethodContext context = new FindPaymentMethodContext();
            PaymentMethodsController controller = new PaymentMethodsController(context);

            PaymentMethodIdViewModel viewModel = new PaymentMethodIdViewModel();
            viewModel.Id = context.PaymentMethod.Id;

            ActionResult result = await controller.Find(viewModel);

            Assert.IsTrue(result is PaymentMethodJson);
        }

        [TestMethod]
        public async Task ListPaymentMethods()
        {
            ListPaymentMethodContext context = new ListPaymentMethodContext();
            PaymentMethodsController controller = new PaymentMethodsController(context);

            PaymentMethodListJson result = (PaymentMethodListJson)await controller.List(new ListPaymentMethodsViewModel());

            Assert.IsTrue(result.PaymentMethods.Count() == context.PaymentMethods.Count());
        }

        [TestMethod]
        public async Task UpdatePaymentMethod()
        {
            UpdatePaymentMethodContext context = new UpdatePaymentMethodContext();
            PaymentMethodsController controller = new PaymentMethodsController(context);

            UpdatePaymentMethodViewModel viewModel = new UpdatePaymentMethodViewModel();
            viewModel.Id = context.PaymentMethod.Id;
            viewModel.Name = context.PaymentMethod.Name + " Updated";
            viewModel.InstallmentLimit = context.PaymentMethod.InstallmentLimit + 1;
            viewModel.DaysToReceive = (context.PaymentMethod.DaysToReceive ?? 0) + 1;
            viewModel.EarlyReceipt = !context.PaymentMethod.EarlyReceipt;
            viewModel.Active = !context.PaymentMethod.Active;

            await controller.Update(viewModel);

            Assert.IsTrue(context.PaymentMethodWasUpdated());
        }
    }
}
