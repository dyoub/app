// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchasedProducts
{
    public class PurchasedProductViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required, Range(0, 999999.99)]
        public decimal? UnitCost { get; set; }

        [Required, Range(0.001, 99999.999)]
        public decimal? Quantity { get; set; }

        [Range(0, 999999.99)]
        public decimal? Discount { get; set; }

        public PurchasedProduct MapTo(PurchasedProduct purchasedProduct)
        {
            purchasedProduct.ProductId = Id.Value;
            purchasedProduct.UnitCost = UnitCost.Value;
            purchasedProduct.Quantity = Quantity.Value;
            purchasedProduct.Discount = Discount;

            return purchasedProduct;
        }
    }
}
