// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.OrderProcessing;
using Dyoub.App.Models.ViewModel.Commercial.RentedProductsReturn;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentedProductsReturnController : TenantController
    {
        public RentedProductsReturnController() { }

        public RentedProductsReturnController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/details/{id:int}/return"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentedProductsReturn/RentedProductsReturnDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}/return"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentedProductsReturn/RentedProductsReturnEdit.cshtml");
        }

        [HttpPost, Route("rent-contracts/return/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(UpdateReturnedProductsViewModel viewModel)
        {
            RentContractProcessing processing = new RentContractProcessing(Tenant);

            bool returned = await processing.Return(
                viewModel.RentContractId.Value,
                viewModel.Date.Value,
                viewModel.MapToRentedProducts()
            );

            if (!returned)
            {
                if (processing.RentContract == null)
                    return this.Error("Rent contract not found.");

                if (!processing.RentContract.Confirmed)
                    return this.Error("Rent contract not confirmed yet."); 

                if (processing.HasInvalidReturnedQuantity)
                    return this.Error("One or more product with returned quantity missing or greater than rented quantity.");
            }

            return this.Success();
        }
    }
}
