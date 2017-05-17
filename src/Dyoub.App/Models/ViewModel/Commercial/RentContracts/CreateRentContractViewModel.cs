// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.RentContracts
{
    public class CreateRentContractViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        public int? WalletId { get; set; }

        [Required]
        public DateTime? StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        [ValidIf]
        public bool StartLowerThanEnd
        {
            get
            {
                if (StartDate == null || EndDate == null)
                {
                    return true;
                }

                return StartDate.Value < EndDate.Value;
            }
        }

        [MaxLength(80)]
        public string Location { get; set; }

        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public RentContract MapTo(RentContract rentContract)
        {
            rentContract.StoreId = StoreId.Value;
            rentContract.WalletId = WalletId;
            rentContract.StartDate = StartDate.Value.Date;
            rentContract.EndDate = EndDate.Value.Date;
            rentContract.Location = Location;
            rentContract.AdditionalInformation = AdditionalInformation;

            return rentContract;
        }
    }
}
