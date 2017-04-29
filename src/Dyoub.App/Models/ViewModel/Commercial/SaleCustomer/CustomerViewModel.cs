// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleCustomer
{
    public class CustomerViewModel
    {
        public int? Id { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; }

        public string NationalId { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string AlternativePhoneNumber { get; set; }
    }
}
