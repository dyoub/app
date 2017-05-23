// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.ServiceModel.OrderProcessing;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.OrderProcessing
{
    public class RentContractProcessingController : TenantController
    {
        public RentContractProcessingController() { }

        public RentContractProcessingController(TenantContext tenant) : base(tenant) { }

        [HttpPost, Route("rent-contracts/confirm"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Confirm(RentContractIdViewModel viewModel)
        {
            RentContractProcessing processing = new RentContractProcessing(Tenant);

            if (!await processing.Confirm(viewModel.Id.Value))
            {
                if (processing.RentContract == null)
                    return this.Error("Rent contract not found.");

                if (processing.RentContract.Confirmed)
                    return this.Error("Rent contract already confirmed.");

                if (processing.HasNoItems)
                    return this.Error("Cannot confirm rent contract without items.");

                if (processing.HasPendingPayment)
                    return this.Error("Cannot confirm rent contract with pending payment.");

                if (processing.ProductConsumption.InsufficientBalance)
                    return this.Error("One or more products with insufficient balance.");
            }

            return new RentContractJson(processing.RentContract);
        }

        [HttpPost, Route("rent-contracts/revert"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Revert(RentContractIdViewModel viewModel)
        {
            RentContractProcessing processing = new RentContractProcessing(Tenant);

            if (!await processing.Revert(viewModel.Id.Value))
            {
                if (processing.RentContract == null)
                {
                    return this.Error("Rent contract not found.");
                }

                if (!processing.RentContract.Confirmed)
                {
                    return this.Error("Cannot revert rent contract not confirmed.");
                }
            }

            return new RentContractJson(processing.RentContract);
        }
    }
}
