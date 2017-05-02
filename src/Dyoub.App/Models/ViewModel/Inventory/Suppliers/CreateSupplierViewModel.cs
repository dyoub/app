// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.Suppliers
{
    public class CreateSupplierViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string NationalId { get; set; }

        [EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [MaxLength(80)]
        public string Homepage { get; set; }

        [MaxLength(80)]
        public string Facebook { get; set; }

        [MaxLength(80)]
        public string Twitter { get; set; }

        [MaxLength(80)]
        public string Instagram { get; set; }

        [MaxLength(80)]
        public string GooglePlus { get; set; }

        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }

        public Supplier MapTo(Supplier supplier)
        {
            supplier.Name = Name;
            supplier.NationalId = NationalId;
            supplier.Email = Email;
            supplier.PhoneNumber = PhoneNumber;
            supplier.AlternativePhoneNumber = AlternativePhoneNumber;
            supplier.Homepage = Homepage;
            supplier.Facebook = Facebook;
            supplier.Twitter = Twitter;
            supplier.Instagram = Instagram;
            supplier.GooglePlus = GooglePlus;

            return supplier;
        }
    }
}
