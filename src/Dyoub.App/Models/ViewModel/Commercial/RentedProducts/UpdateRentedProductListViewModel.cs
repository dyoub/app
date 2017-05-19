// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.RentedProducts
{
    public class UpdateRentedProductListViewModel
    {
        [Required]
        public int? RentContractId { get; set; }

        public List<RentedProductViewModel> Products { get; private set; }

        [ValidIf]
        public bool HasAtLeastOneItem
        {
            get { return Products.Any(); }
        }

        public UpdateRentedProductListViewModel()
        {
            Products = new List<RentedProductViewModel>();
        }

        public IEnumerable<RentedProduct> Map()
        {
            return Products.Select(product => product.MapTo(new RentedProduct
            {
                RentContractId = RentContractId.Value
            })).ToList();
        }
    }
}
