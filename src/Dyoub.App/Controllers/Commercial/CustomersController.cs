// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.ViewModel.Commercial.Customers;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Commercial.Customers;
using System.Collections.Generic;

namespace Dyoub.App.Controllers.Commercial
{
    public class CustomersController : TenantController
    {
        public CustomersController() { }

        public CustomersController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("customers/new"), Authorization(Scope = "customers.edit")]
        public ActionResult Add()
        {
            return View("~/Views/Commercial/Customers/CustomerEdit.cshtml");
        }

        [HttpGet, Route("customers/details/{id:int}"), Authorization(Scope = "customers.read")]
        public ActionResult Details(int id)
        {
            return View("~/Views/Commercial/Customers/CustomerDetails.cshtml");
        }

        [HttpGet, Route("customers/edit/{id:int}"), Authorization(Scope = "customers.edit")]
        public ActionResult Edit(int id)
        {
            return View("~/Views/Commercial/Customers/CustomerEdit.cshtml");
        }

        [HttpGet, Route("customers"), Authorization(Scope = "customers.read")]
        public ActionResult Index()
        {
            return View("~/Views/Commercial/Customers/CustomerList.cshtml");
        }

        [HttpPost, Route("customers/create"), Authorization(Scope = "customers.edit")]
        public async Task<ActionResult> Create(CreateCustomerViewModel viewModel)
        {
            Tenant.Customers.Add(viewModel.MapTo(new Customer()));
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("customers/delete"), Authorization(Scope = "customers.edit")]
        public async Task<ActionResult> Delete(CustomerIdViewModel viewModel)
        {
            Customer customer = await Tenant.Set<Customer>()
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (customer == null)
            {
                return this.Error("Customer not found.");
            }

            if (await Tenant.SaleOrders.WhereCustomerId(customer.Id).AnyAsync())
            {
                return this.Error("This customer has associated sale orders.");
            }

            Tenant.Customers.Remove(customer);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }

        [HttpPost, Route("customers/find"), Authorization(Scope = "customers.read")]
        public async Task<ActionResult> Find(CustomerIdViewModel viewModel)
        {
            Customer customer = await Tenant.Customers
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            return new CustomerJson(customer);
        }

        [HttpPost, Route("customers"), Authorization(Scope = "customers.read")]
        public async Task<ActionResult> List(ListCustomersViewModel viewModel)
        {
            ICollection<Customer> customers = await Tenant.Customers
                .WhereNameContains(viewModel.Name.Words())
                .OrderedByName()
                .Paginate(viewModel.Index)
                .ToListAsync();

            return new CustomerListJson(customers);
        }

        [HttpPost, Route("customers/update"), Authorization(Scope = "customers.edit")]
        public async Task<ActionResult> Update(UpdateCustomerViewModel viewModel)
        {
            Customer customer = await Tenant.Customers
                .WhereId(viewModel.Id.Value)
                .SingleOrDefaultAsync();

            if (customer == null)
            {
                return this.Error("Customer not found.");
            }

            viewModel.MapTo(customer);
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
