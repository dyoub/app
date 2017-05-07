// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchaseSupplier
{
    public class PurchaseSupplierViewModel
    {
        [Required]
        public int? PurchaseOrderId { get; set; }
        
        public int? SupplierId { get; set; }
    }
}
