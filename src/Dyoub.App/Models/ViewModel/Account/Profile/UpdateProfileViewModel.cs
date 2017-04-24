// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Users;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Account.Profile
{
    public class UpdateProfileViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public void MapTo(User user)
        {
            user.Name = Name;
        }
    }
}
