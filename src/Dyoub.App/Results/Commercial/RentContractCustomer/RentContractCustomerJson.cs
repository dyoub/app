// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentContractCustomer
{
    public class RentContractCustomerJson : JsonResult
    {
        public RentContract RentContract { get; private set; }

        public RentContractCustomerJson(RentContract saleOrder)
        {
            RentContract = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                confirmed = RentContract.Confirmed,
                returnPending = RentContract.ReturnPending,
                customer = RentContract.Customer == null ? null : new
                {
                    id = RentContract.Customer.Id,
                    name = RentContract.Customer.Name,
                    nationalId = RentContract.Customer.NationalId,
                    email = RentContract.Customer.Email,
                    phoneNumber = RentContract.Customer.PhoneNumber,
                    alternativePhoneNumber = RentContract.Customer.AlternativePhoneNumber
                }
            };

            base.ExecuteResult(context);
        }
    }
}
