// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.RentContractPartners;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.RentContractPartners
{
    public class UpdateRentContractPartnerListViewModel
    {
        [Required]
        public int? RentContractId { get; set; }

        public List<int> Partners { get; private set; }

        [ValidIf]
        public bool NoDuplicatePartnerId
        {
            get
            {
                return !Partners.HasDuplicate(id => id);
            }
        }

        public UpdateRentContractPartnerListViewModel()
        {
            Partners = new List<int>();
        }

        public IEnumerable<RentContractPartner> MapTo()
        {
            return Partners.Select(partnerId => new RentContractPartner
            {
                RentContractId = RentContractId.Value,
                PartnerId = partnerId
            });
        }
    }
}
