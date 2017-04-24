// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.Profile
{
    public class ChangePasswordViewModel
    {
        [Required, MaxLength(20)]
        public string OldPassword { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string NewPassword { get; set; }
    }
}
