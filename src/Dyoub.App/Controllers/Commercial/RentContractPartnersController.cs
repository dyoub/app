// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.App.Models.EntityModel.Commercial.RentContractPartners;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.ViewModel.Commercial.RentContractPartners;
using Dyoub.App.Models.ViewModel.Commercial.RentContracts;
using Dyoub.App.Results.Commercial.RentContractPartners;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class RentContractPartnersController : TenantController
    {
        public RentContractPartnersController() { }

        public RentContractPartnersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("rent-contracts/details/{id:int}/partners"), Authorization(Scope = "rent-contracts.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/RentContractPartners/RentContractPartnersDetails.cshtml");
        }

        [HttpGet, Route("rent-contracts/edit/{id:int}/partners"), Authorization(Scope = "rent-contracts.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/RentContractPartners/RentContractPartnersEdit.cshtml");
        }

        [HttpPost, Route("rent-contracts/partners"), Authorization(Scope = "rent-contracts.read")]
        public async Task<ActionResult> List(RentContractIdViewModel viewModel)
        {
            RentContract rentContract = await Tenant.RentContracts
                .WhereId(viewModel.Id.Value)
                .IncludeRentContractPartners()
                .SingleOrDefaultAsync();
            
            return new RentContractPartnerListJson(rentContract);
        }

        [HttpPost, Route("rent-contracts/partners/update"), Authorization(Scope = "rent-contracts.edit")]
        public async Task<ActionResult> Update(UpdateRentContractPartnerListViewModel viewModel)
        {
            if (!await Tenant.RentContracts.WhereId(viewModel.RentContractId.Value).AnyAsync())
                return this.Error("Rent contract not found.");

            int partnerCount = await Tenant.Partners
                .WhereIdIn(viewModel.Partners)
                .CountAsync();

            if (viewModel.Partners.Count != partnerCount)
                return this.Error("One or more partners not found.");

            ICollection<RentContractPartner> rentContractPartners = await Tenant.RentContractPartners
                .WhereRentContractId(viewModel.RentContractId.Value)
                .ToListAsync();

            Tenant.RentContractPartners.RemoveRange(rentContractPartners);
            Tenant.RentContractPartners.AddRange(viewModel.MapTo());
            
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
