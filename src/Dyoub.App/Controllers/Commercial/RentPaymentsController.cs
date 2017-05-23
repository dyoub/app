// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Commercial.RentPayments;
using Dyoub.App.Models.ViewModel.Commercial.RentPayments;
using Dyoub.App.Models.ServiceModel.Commercial;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentPaymentsController : TenantController
    {
        public RentPaymentsController() { }

        public RentPaymentsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/details/{id:int}/payments"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentPayments/RentPaymentsDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}/payments"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentPayments/RentPaymentsEdit.cshtml");
        }

        [HttpPost, Route("rent-contracts/payments"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> List(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .IncludePaymentMethods()
                .IncludeRentIncomes()
                .SingleOrDefaultAsync();

            return new RentPaymentListJson(rentContract);
        }

        [HttpPost, Route("rent-contracts/payments/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(UpdateRentPaymentsViewModel viewModel)
        {
            RentPromisedPayments promisedPayments = new RentPromisedPayments(Tenant);

            bool registered = await promisedPayments.RegisterPayments(
                viewModel.RentContractId.Value,
                viewModel.MapRentPayments(),
                viewModel.Discount
            );

            if (!registered)
            {
                if (promisedPayments.RentContract == null)
                {
                    return this.Error("Rent contract not found.");
                }

                if (promisedPayments.RentContract.Confirmed)
                {
                    return this.Error("Rent contract already confirmed.");
                }

                if (promisedPayments.HasPendingPayment)
                {
                    return this.Error("Rent contract has pending payment.");
                }
            }

            return this.Success();
        }
    }
}
