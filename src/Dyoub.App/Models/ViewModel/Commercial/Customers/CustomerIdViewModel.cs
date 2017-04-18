// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.Customers
{
    public class CustomerIdViewModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
