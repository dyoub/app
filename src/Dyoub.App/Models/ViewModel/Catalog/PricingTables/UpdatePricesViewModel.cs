// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Catalog.PricingTables
{
    public class UpdatePricesViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        public ICollection<ItemPriceViewModel> Items { get; private set; }
        
        public UpdatePricesViewModel()
        {
            Items = new List<ItemPriceViewModel>();
        }

        public ICollection<ItemPrice> MapToItemPrices()
        {
            return Items.Select(item => item.MapToItemPrice(StoreId.Value)).ToList();
        }
    }
}
