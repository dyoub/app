// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchasedProducts
{
    public class UpdatePurchasedProductListViewModel
    {
        [Required]
        public int? PurchaseOrderId { get; set; }

        public List<PurchasedProductViewModel> Products { get; private set; }

        [ValidIf]
        public bool HasAtLeastOneItem
        {
            get { return Products.Any(); }
        }

        public UpdatePurchasedProductListViewModel()
        {
            Products = new List<PurchasedProductViewModel>();
        }

        public IEnumerable<PurchasedProduct> Map()
        {
            return Products.Select(product => product.MapTo(new PurchasedProduct
            {
                PurchaseOrderId = PurchaseOrderId.Value
            })).ToList();
        }
    }
}
