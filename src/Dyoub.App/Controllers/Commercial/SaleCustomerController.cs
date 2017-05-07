// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.ViewModel.Commercial.SaleCustomer;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using Dyoub.App.Results.Commercial.SaleCustomer;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyoub.App.Controllers.Commercial
{
    public class SaleCustomerController : TenantController
    {
        public SaleCustomerController() { }

        public SaleCustomerController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("sale-orders/details/{id:int}/customer"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/SaleCustomer/SaleCustomerDetails.cshtml");
        }

        [HttpGet, Route("sale-orders/edit/{id:int}/customer"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/SaleCustomer/SaleCustomerEdit.cshtml");
        }

        [HttpPost, Route("sale-orders/customer"), Authorization(Scope = "sale-orders.read")]
        public async Task<ActionResult> Find(SaleOrderIdViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .IncludeCustomer()
                .SingleOrDefaultAsync();

            return new SaleCustomerJson(saleOrder);
        }

        [HttpPost, Route("sale-orders/customer/update"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Update(SaleCustomerViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.SaleOrderId.Value)
                .SingleOrDefaultAsync();

            if (saleOrder == null)
            {
                return this.Error("Sale order not found.");
            }

            if (saleOrder.Confirmed)
            {
                return this.Error("Sale order already confirmed.");
            }

            if (viewModel.CustomerId != null)
            {
                if (!await Tenant.Customers.WhereId(viewModel.CustomerId.Value).AnyAsync())
                {
                    return this.Error("Customer not found.");
                }
            }
            
            saleOrder.CustomerId = viewModel.CustomerId;
            await Tenant.SaveChangesAsync();

            return this.Success();
        }
    }
}
