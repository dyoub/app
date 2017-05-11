// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Commercial.SaleItems
{
    public class UpdateSaleItemListViewModel
    {
        [Required]
        public int? SaleOrderId { get; set; }

        public List<SaleItemViewModel> Items { get; private set; }
        
        [ValidIf]
        public bool HasAtLeastOneItem
        {
            get { return Items.Any(); }
        }

        public UpdateSaleItemListViewModel()
        {
            Items = new List<SaleItemViewModel>();
        }

        public IEnumerable<SaleItem> Map()
        {
            return Items.Select(item => item.Map()).ToList();
        }
    }
}
