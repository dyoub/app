// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SaleCustomer
{
    public class SaleCustomerJson : JsonResult
    {
        public SaleOrder SaleOrder { get; private set; }

        public SaleCustomerJson(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = SaleOrder == null ? null : new
            {
                id = SaleOrder.Id,
                confirmed = SaleOrder.Confirmed,
                customer = SaleOrder.Customer == null ? null : new
                {
                    id = SaleOrder.Customer.Id,
                    name = SaleOrder.Customer.Name,
                    nationalId = SaleOrder.Customer.NationalId,
                    email = SaleOrder.Customer.Email,
                    phoneNumber = SaleOrder.Customer.PhoneNumber,
                    alternativePhoneNumber = SaleOrder.Customer.AlternativePhoneNumber
                }
            };

            base.ExecuteResult(context);
        }
    }
}
