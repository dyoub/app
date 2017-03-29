// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.Users
{
    public class SigninViewModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(80)]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
    }
}
