// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.ProductStock
{
    public class ProductStockIdViewModel
    {
        [Required]
        public int? StoreId { get; set; }
    }
}
