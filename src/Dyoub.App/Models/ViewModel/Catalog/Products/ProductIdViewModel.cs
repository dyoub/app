// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.Products
{
    public class ProductIdViewModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
