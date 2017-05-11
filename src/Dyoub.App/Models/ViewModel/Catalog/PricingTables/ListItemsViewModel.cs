// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.PricingTables
{
    public class ListItemsViewModel
    {
        public enum Filter { All, Products, Services }

        [Required]
        public int? StoreId { get; set; }
        
        public string NameOrCode { get; set; }

        public Filter SearchFor { get; set; }

        public int Index { get; set; }

        public ListItemsViewModel()
        {
            SearchFor = Filter.All;
        }
    }
}
