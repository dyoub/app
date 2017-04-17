// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.PricingTables
{
    public class ItemPriceViewModel
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

        [Range(0, 999999.99)]
        public decimal? UnitPrice { get; set; }

        public ItemPrice MapToItemPrice(int storeId)
        {
            return new ItemPrice
            {
                StoreId = storeId,
                ProductId = IsProduct ? Id : null,
                ServiceId = IsService ? Id : null,
                UnitPrice = UnitPrice
            };
        }
    }
}
