// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.Partners;
using Dyoub.App.Models.ViewModel.Commercial.Partners;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Commercial.Partners;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Commercial
{
    public class PartnersController : TenantController
    {
        public PartnersController() { }

        public PartnersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("partners/new"), Authorization(Scope = "partners.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Commercial/Partners/PartnerEdit.cshtml");
        }

        [HttpGet, Route("partners/details/{id:int}"), Authorization(Scope = "partners.read")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/Partners/PartnerDetails.cshtml");
        }

        [HttpGet, Route("partners/edit/{id:int}"), Authorization(Scope = "partners.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/Partners/PartnerEdit.cshtml");
        }

        [HttpGet, Route("partners"), Authorization(Scope = "partners.read")]
        public ActionResult Index()
        {
            return View("~/Views/Commercial/Partners/PartnerList.cshtml");
        }

        [HttpPost, Route("partners/create"), Authorization(Scope = "partners.edit")]
        public async Task<ActionResult> Create(CreatePartnerViewModel viewModel)
        {
            Partner partner = Tenant.Partners.Add(viewModel.MapTo(new Partner()));
            await Tenant.SaveChangesAsync();

            return new PartnerJson(partner);
        }

        [HttpPost, Route("partners/delete"), Authorization(Scope = "partners.edit")]
        public async Task<ActionResult> Delete(PartnerIdViewModel viewModel)
        {
            Partner partner = await Tenant.Set<Partner>()
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (partner == null)
            {
                return this.Error("Partner not found.");
            }

            Tenant.Partners.Remove(partner);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("partners/find"), Authorization(Scope = "partners.read")]
        public async Task<ActionResult> Find(PartnerIdViewModel viewModel)
        {
            Partner partner = await Tenant.Partners
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new PartnerJson(partner);
        }

        [HttpPost, Route("partners"), Authorization(Scope = "partners.read")]
        public async Task<ActionResult> List(ListPartnersViewModel viewModel)
        {
            ICollection<Partner> partners = await Tenant.Partners
                .WhereNameContains(viewModel.Name.Words())
                .OrderedByName()
                .Paginate(viewModel.Index, viewModel.Limit)
                .ToListAsync();

            return new PartnerListJson(partners);
        }

        [HttpPost, Route("partners/update"), Authorization(Scope = "partners.edit")]
        public async Task<ActionResult> Update(UpdatePartnerViewModel viewModel)
        {
            Partner partner = await Tenant.Partners
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (partner == null)
            {
                return this.Error("Partner not found.");
            }

            viewModel.MapTo(partner);
            await Tenant.SaveChangesAsync();

            return new PartnerJson(partner);
        }
    }
}
