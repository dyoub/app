// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.RentedProducts
{
    public class RentedProductViewModel
    {
        [Required]
        public int? Id { get; set; }
        
        [Required, Range(0.001, 99999.999)]
        public decimal? Quantity { get; set; }

        [Range(0, 999999.99)]
        public decimal? Discount { get; set; }

        public RentedProduct MapTo(RentedProduct purchasedProduct)
        {
            purchasedProduct.ProductId = Id.Value;
            purchasedProduct.Quantity = Quantity.Value;
            purchasedProduct.Discount = Discount;

            return purchasedProduct;
        }
    }
}
