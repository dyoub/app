// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.Customers
{
    public class CustomerJson : JsonResult
    {
        public Customer Customer { get; private set; }

        public CustomerJson(Customer customer)
        {
            Customer = customer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Customer != null)
            {
                Data = new
                {
                    id = Customer.Id,
                    name = Customer.Name,
                    nationalId = Customer.NationalId,
                    email = Customer.Email,
                    phoneNumber = Customer.PhoneNumber,
                    alternativePhoneNumber = Customer.AlternativePhoneNumber
                };
            }

            base.ExecuteResult(context);
        }
    }
}
