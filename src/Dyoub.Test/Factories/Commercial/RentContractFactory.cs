// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;

namespace Dyoub.Test.Factories.Commercial
{
    public class RentContractFactory
    {
        public static RentContract RentContract(Store store, Customer customer = null, Wallet wallet = null)
        {
            return new RentContract
            {
                Store = store,
                Tenant = store.Tenant,
                Customer = customer,
                Wallet = wallet,
                CreatedAt = DateTime.UtcNow,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(1),
                Location = "Brazil",
                AdditionalInformation = "Additional information.",
                Author = "user@email.com",
                Total = 10.00M,
                Discount = 10,
                TotalPayable = 9.00M
            };
        }

        public static RentContract ConfirmedRentContract(Store store, Customer customer = null)
        {
            RentContract rentContract = RentContract(store, customer);
            rentContract.ConfirmationDate = DateTime.UtcNow;

            return rentContract;
        }
    }
}
