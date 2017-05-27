// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.RentedProductsReturn
{
    public class UpdateReturnedProductsViewModel
    {
        [Required]
        public int? RentContractId { get; set; }

        [Required]
        public DateTime? DateOfReturn { get; set; }

        [Required]
        public TimeSpan? TimeOfReturn { get; set; }

        [Required]
        public DateTime? Date
        {
            get
            {
                if (DateOfReturn == null || TimeOfReturn == null)
                {
                    return null;
                }

                return DateOfReturn.Value.Add(TimeOfReturn.Value);
            }
        }

        public List<ReturnedProductViewModel> Products { get; private set; }

        public UpdateReturnedProductsViewModel()
        {
            Products = new List<ReturnedProductViewModel>();
        }

        public IEnumerable<RentedProduct> MapToRentedProducts()
        {
            return Products.Select(viewModel => viewModel.MapToRentedProduct()).ToList();
        }
    }
}
