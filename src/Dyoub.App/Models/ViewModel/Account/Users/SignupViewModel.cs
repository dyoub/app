// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Users;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.Users
{
    public class SignupViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [Required, EmailAddress, MaxLength(80)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(20)]
        public string Password { get; set; }

        public User MapToUser()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password
            };
        }
    }
}
