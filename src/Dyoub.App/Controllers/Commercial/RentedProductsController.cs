// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.ServiceModel.Commercial;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Models.ViewModel.Commercial.RentedProducts;
using Dyoub.App.Results.Commercial.RentedProducts;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentedProductsController : TenantController
    {
        public RentedProductsController() { }

        public RentedProductsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/details/{id:int}/products"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentedProducts/RentedProductsDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}/products"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentedProducts/RentedProductsEdit.cshtml");
        }

        [HttpPost, Route("rent-contracts/products"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> List(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .IncludeRentedProducts()
                .SingleOrDefaultAsync();
            
            return new RentedProductListJson(rentContract);
        }

        [HttpPost, Route("rent-contracts/products/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(UpdateRentedProductListViewModel viewModel)
        {
            RentShoppingCart shoppingCart = new RentShoppingCart(Tenant);

            if (!await shoppingCart.Checkout(viewModel.RentContractId.Value, viewModel.Map()))
            {
                if (shoppingCart.RentContract == null)
                    return this.Error("Rent contract not found.");

                if (shoppingCart.RentContract.Confirmed)
                    return this.Error("Rent contract already confirmed.");

                if (shoppingCart.HasOneOrMoreProductsNotFound)
                    return this.Error("One or more products not found.");

                if (shoppingCart.HasOneOrMoreProductsNotMarketed)
                    return this.Error("One or more products not marketed.");

                if (shoppingCart.HasProductWithTotalNegative)
                    return this.Error("One or more products have total negative.");
            }

            return this.Success();
        }
    }
}
