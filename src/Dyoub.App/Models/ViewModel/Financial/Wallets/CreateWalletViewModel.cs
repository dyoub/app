// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Financial.Wallets;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Financial.Wallets
{
    public class CreateWalletViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string AdditionalInformation { get; set; }
        
        public bool Active { get; set; }

        public Wallet MapTo(Wallet wallet)
        {
            wallet.Name = Name;
            wallet.AdditionalInformation = AdditionalInformation;
            wallet.Active = Active;

            return wallet;
        }
    }
}
