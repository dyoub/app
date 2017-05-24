// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.Partners;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.Partners
{
    public class CreatePartnerViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string NationalId { get; set; }

        [EmailAddress, MaxLength(80)]
        public string Email { get; set; }
        
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string AlternativePhoneNumber { get; set; }

        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public Partner MapTo(Partner partner)
        {
            partner.Name = Name;
            partner.NationalId = NationalId;
            partner.Email = Email;
            partner.PhoneNumber = PhoneNumber;
            partner.AlternativePhoneNumber = AlternativePhoneNumber;
            partner.AdditionalInformation = AdditionalInformation;

            return partner;
        }
    }
}
