// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.ViewModel.Commercial.SaleOrders;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Dyoub.App.Results.Commercial.SalePayments;
using Dyoub.App.Models.ViewModel.Commercial.SalePayments;
using Dyoub.App.Models.ServiceModel.Commercial;

namespace Dyoub.App.Controllers.Commercial
{
    public class SalePaymentsController : TenantController
    {
        public SalePaymentsController() { }

        public SalePaymentsController(TenantContext tenant) : base(tenant) { }

        [HttpGet, Route("sale-orders/details/{id:int}/payments"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Details()
        {
            return View("~/Views/Commercial/SalePayments/SalePaymentsDetails.cshtml");
        }

        [HttpGet, Route("sale-orders/edit/{id:int}/payments"), Authorization(Scope = "sale-orders.edit")]
        public ActionResult Edit()
        {
            return View("~/Views/Commercial/SalePayments/SalePaymentsEdit.cshtml");
        }

        [HttpPost, Route("sale-orders/payments"), Authorization(Scope = "sale-orders.read")]
        public async Task<ActionResult> List(SaleOrderIdViewModel viewModel)
        {
            SaleOrder saleOrder = await Tenant.SaleOrders
                .WhereId(viewModel.Id.Value)
                .IncludePaymentMethods()
                .IncludeSaleIncomes()
                .SingleOrDefaultAsync();

            return new SalePaymentListJson(saleOrder);
        }

        [HttpPost, Route("sale-orders/payments/update"), Authorization(Scope = "sale-orders.edit")]
        public async Task<ActionResult> Update(UpdateSalePaymentsViewModel viewModel)
        {
            CashRegister cashRegister = new CashRegister(Tenant);

            bool registered = await cashRegister.RegisterPayment(
                viewModel.SaleOrderId.Value,
                viewModel.MapSalePayments(),
                viewModel.Discount
            );

            if (!registered)
            {
                if (cashRegister.SaleOrder == null)
                {
                    return this.Error("Sale order not found.");
                }

                if (cashRegister.SaleOrder.Confirmed)
                {
                    return this.Error("Sale order already confirmed.");
                }

                if (cashRegister.HasPendingPayment)
                {
                    return this.Error("Sale order has pending payment.");
                }
            }
            
            return this.Success();
        }
    }
}
