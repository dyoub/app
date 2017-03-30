// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.PasswordRecoveries
{
    public class CreatePasswordRecoveryViewModel
    {
        [Required, EmailAddress, MaxLength(80)]
        public string Email { get; set; }
    }
}
