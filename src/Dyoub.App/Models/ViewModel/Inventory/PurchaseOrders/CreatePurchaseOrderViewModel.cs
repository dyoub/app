// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders
{
    public class CreatePurchaseOrderViewModel
    {
        [Required]
        public int? StoreId { get; set; }

        public int? WalletId { get; set; }

        public string InvoiceNumber { get; set; }

        [Required]
        public DateTime? IssueDate { get; set; }

        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public PurchaseOrder MapTo(PurchaseOrder purchaseOrder)
        {
            purchaseOrder.StoreId = StoreId.Value;
            purchaseOrder.WalletId = WalletId;
            purchaseOrder.InvoiceNumber = InvoiceNumber;
            purchaseOrder.IssueDate = IssueDate.Value.Date;
            purchaseOrder.AdditionalInformation = AdditionalInformation;

            return purchaseOrder;
        }
    }
}
