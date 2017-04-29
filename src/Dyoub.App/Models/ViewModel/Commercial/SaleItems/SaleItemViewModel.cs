// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleItems
{
    public class SaleItemViewModel
    {
        [Required]
        public int? Id { get; set; }

        public bool IsProduct { get; set; }

        public bool IsService { get; set; }

        [ValidIf]
        public bool IsProductOrService
        {
            get
            {
                return (IsProduct && !IsService) || (IsService && !IsProduct);
            }
        }

        [Required, Range(0.001, 99999.999)]
        public decimal? Quantity { get; set; }

        [Range(0, 999999.99)]
        public decimal? Discount { get; set; }

        public SaleItem Map()
        {
            return new SaleItem
            {
                Id = Id.Value,
                IsProduct = IsProduct,
                IsService = IsService,
                Quantity = Quantity.Value,
                Discount = Discount
            };
        }
    }
}
