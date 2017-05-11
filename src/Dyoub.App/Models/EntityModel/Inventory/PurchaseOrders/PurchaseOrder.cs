// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders
{
    public class PurchaseOrder : ITenantData
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int StoreId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int? SupplierId { get; set; }
        public int? WalletId { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Taxes { get; set; }
        public decimal TotalPayable { get; set; }
        public decimal TotalCost { get; set; }
        public string Author { get; set; }
        public string AdditionalInformation { get; set; }
        public bool Confirmed { get { return ConfirmationDate != null; } }
        public bool Budget
        {
            get
            {
                return !Confirmed && Total > 0 && new Money(Total)
                    .SubtractPercentage(Discount ?? 0) == TotalPayable;
            }
        }
        public bool Draft
        {
            get
            {
                return !Confirmed && (Total == 0 || new Money(Total)
                    .SubtractPercentage(Discount ?? 0) != TotalPayable);
            }
        }

        public virtual Tenant Tenant { get; set; }
        public virtual Store Store { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<PurchasedProduct> PurchasedProducts { get; set; }
    }
}
