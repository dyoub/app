// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.ViewModel.Commercial.PaymentMethods;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Commercial.PaymentMethods;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Commercial
{
    public class PaymentMethodsController : TenantController
    {
        public PaymentMethodsController() { }

        public PaymentMethodsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("payment-methods/new"), Authorization(Scope = "payment-methods.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Commercial/PaymentMethods/PaymentMethodEdit.cshtml");
        }

        [HttpGet, Route("payment-methods/details/{id:int}"), Authorization(Scope = "payment-methods.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/PaymentMethods/PaymentMethodDetails.cshtml");
        }

        [HttpGet, Route("payment-methods/edit/{id:int}"), Authorization(Scope = "payment-methods.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/PaymentMethods/PaymentMethodEdit.cshtml");
        }

        [HttpGet, Route("payment-methods"), Authorization(Scope = "payment-methods.read")]
        public ActionResult Index()
        {
            return View("~/Views/Commercial/PaymentMethods/PaymentMethodList.cshtml");
        }

        [HttpPost, Route("payment-methods/create"), Authorization(Scope = "payment-methods.edit")]
        public async Task<ActionResult> Create(CreatePaymentMethodViewModel viewModel)
        {
            Tenant.PaymentMethods.Add(viewModel.MapTo(new PaymentMethod()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("payment-methods/delete"), Authorization(Scope = "payment-methods.edit")]
        public async Task<ActionResult> Delete(PaymentMethodIdViewModel viewModel)
        {
            PaymentMethod paymentMethod = await Tenant.PaymentMethods
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (paymentMethod == null)
            {
                return this.Error("Payment method not found.");
            }

            Tenant.PaymentMethods.Remove(paymentMethod);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("payment-methods/find"), Authorization(Scope = "payment-methods.read")]
        public async Task<ActionResult> Find(PaymentMethodIdViewModel viewModel)
        {
            PaymentMethod paymentMethod = await Tenant.PaymentMethods
                .WhereId(viewModel.Id.Value)
                .IncludeFees()
                .SingleOrDefaultAsync();

            return new PaymentMethodJson(paymentMethod);
        }

        [HttpPost, Route("payment-methods"), Authorization(Scope = "payment-methods.read")]
        public async Task<ActionResult> List(ListPaymentMethodsViewModel viewModel)
        {
            ICollection<PaymentMethod> paymentMethods = await Tenant.PaymentMethods
                .WhereNameContains(viewModel.Name.Words())
                .OrderByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new PaymentMethodListJson(paymentMethods);
        }

        [HttpPost,Route("payment-methods/actives"), Authorization(Scope = "payment-methods.read")]
        public async Task<ActionResult> ListActives()
        {
            ICollection<PaymentMethod> paymentMethods = await Tenant.PaymentMethods
                .Actives()
                .OrderByName()
                .ToListAsync();

            return new PaymentMethodListJson(paymentMethods);
        }

        [HttpPost, Route("payment-methods/update"), Authorization(Scope = "payment-methods.edit")]
        public async Task<ActionResult> Update(UpdatePaymentMethodViewModel viewModel)
        {
            PaymentMethod paymentMethod = await Tenant.PaymentMethods
                .WhereId(viewModel.Id.Value)
                .IncludeFees()
                .SingleOrDefaultAsync();

            if (paymentMethod == null)
            {
                return this.Error("Payment method not found.");
            }

            Tenant.PaymentMethodFees.RemoveRange(paymentMethod.PaymentMethodFees);

            viewModel.MapTo(paymentMethod);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
