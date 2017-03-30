// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.PasswordRecoveries
{
    public class UpdatePasswordRecoveryViewModel
    {
        [Required]
        public string Token { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string NewPassword { get; set; }
    }
}
