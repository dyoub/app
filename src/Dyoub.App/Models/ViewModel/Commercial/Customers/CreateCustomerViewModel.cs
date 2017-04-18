// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.Customers
{
    public class CreateCustomerViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public string NationalId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternativePhoneNumber { get; set; }

        public Customer MapTo(Customer customer)
        {
            customer.Name = Name;
            customer.NationalId = NationalId;
            customer.Email = Email;
            customer.PhoneNumber = PhoneNumber;
            customer.AlternativePhoneNumber = AlternativePhoneNumber;

            return customer;
        }
    }
}
