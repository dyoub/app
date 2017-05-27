// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.RentedProductsReturn
{
    public class ReturnedProductViewModel
    {
        [Required]
        public int? Id { get; set; }

        [Required, Range(0, 99999.999)]
        public decimal? ReturnedQuantity { get; set; }

        public RentedProduct MapToRentedProduct()
        {
            return new RentedProduct
            {
                ProductId = Id.Value,
                ReturnedQuantity = ReturnedQuantity.Value
            };
        }
    }
}
