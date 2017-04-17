// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.PricingTables
{
    public class ListItemsForSaleViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        public string NameOrCode { get; set; }
    }
}
