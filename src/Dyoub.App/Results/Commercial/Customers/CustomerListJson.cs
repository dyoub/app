// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace Dyoub.App.Results.Commercial.Customers
{
    public class CustomerListJson : JsonResult
    {
        public IEnumerable<Customer> Customers { get; private set; }

        public CustomerListJson(IEnumerable<Customer> customers)
        {
            Customers = customers;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Customers.Select(customer => new
            {
                id = customer.Id,
                name = customer.Name,
                nationalId = customer.NationalId,
                email = customer.Email,
                phoneNumber = customer.PhoneNumber,
                alternativePhoneNumber = customer.AlternativePhoneNumber
            });

            base.ExecuteResult(context);
        }
    }
}
