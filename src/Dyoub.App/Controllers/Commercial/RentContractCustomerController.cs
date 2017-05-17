// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.ViewModel.Commercial.RentContractCustomer;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContractCustomer;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentContractCustomerController : TenantController
    {
        public RentContractCustomerController() { }

        public RentContractCustomerController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/details/{id:int}/customer"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentContractCustomer/RentContractCustomerDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}/customer"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentContractCustomer/RentContractCustomerEdit.cshtml");
        }

        [HttpPost, Route("rent-contracts/customer"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> Find(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .IncludeCustomer()
                .SingleOrDefaultAsync();

            return new RentContractCustomerJson(rentContract);
        }

        [HttpPost, Route("rent-contracts/customer/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(RentContractCustomerViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.RentContractId.Value)
                .SingleOrDefaultAsync();

            if (rentContract == null)
            {
                return this.Error("Rent contract not found.");
            }

            if (rentContract.Confirmed)
            {
                return this.Error("Rent contract already confirmed.");
            }

            if (viewModel.CustomerId != null)
            {
                if (!await Tenant.Customers.WhereId(viewModel.CustomerId.Value).AnyAsync())
                {
                    return this.Error("Customer not found.");
                }
            }

            rentContract.CustomerId = viewModel.CustomerId;
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
