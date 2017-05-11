// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.ViewModel.Catalog.Services;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Catalog.Services;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Catalog
{
    public class ServicesController : TenantController
    {
        public ServicesController() { }

        public ServicesController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("services/new"), Authorization(Scope = "services.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Catalog/Services/ServiceEdit.cshtml");
        }

        [HttpGet, Route("services/details/{id:int}"), Authorization(Scope = "services.read")]
        public ActionResult Details()
        {
            return View("~/Views/Catalog/Services/ServiceDetails.cshtml");
        }

        [HttpGet, Route("services/edit/{id:int}"), Authorization(Scope = "services.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Catalog/Services/ServiceEdit.cshtml");
        }

        [HttpGet, Route("services"), Authorization(Scope = "services.read")]
        public ActionResult Index()
        {
            return View("~/Views/Catalog/Services/ServiceList.cshtml");
        }

        [HttpPost, Route("services/create"), Authorization(Scope = "services.edit")]
        public async Task<ActionResult> Create(CreateServiceViewModel viewModel)
        {
            Tenant.Services.Add(viewModel.MapTo(new Service()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("services/delete"), Authorization(Scope = "services.edit")]
        public async Task<ActionResult> Delete(ServiceIdViewModel viewModel)
        {
            Service service = await Tenant.Services
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (service == null)
            {
                return this.Error("Service not found.");
            }

            Tenant.Services.Remove(service);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("services/find"), Authorization(Scope = "services.read")]
        public async Task<ActionResult> Find(ServiceIdViewModel viewModel)
        {
            Service service = await Tenant.Services
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new ServiceJson(service);
        }

        [HttpPost, Route("services"), Authorization(Scope = "services.read")]
        public async Task<ActionResult> List(ListServicesViewModel viewModel)
        {
            ICollection<Service> services = await Tenant.Services
                .OrderByName()
                .WhereNameOrCode(viewModel.NameOrCode.Words())
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new ServiceListJson(services);
        }

        [HttpPost, Route("services/update"), Authorization(Scope = "services.edit")]
        public async Task<ActionResult> Update(UpdateServiceViewModel viewModel)
        {
            Service service = await Tenant.Services
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (service == null)
            {
                return this.Error("Service not found.");
            }

            viewModel.MapTo(service);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
