// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Inventory;
using System;

namespace Dyoub.App.Models.EntityModel.Commercial.RentedProducts
{
    public class RentedProduct : ITenantData, IIncomingProduct, IOutgoingProduct
    {
        public int RentContractId { get; set; }
        public int ProductId { get; set; }
        public int TenantId { get; set; }
        public decimal Quantity { get; set; }
        public decimal? ReturnedQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalPayable { get; set; }
        public Guid? StockTransactionIdIn { get; set; }
        public Guid? StockTransactionIdOut { get; set; }

        public virtual RentContract RentContract { get; set; }
        public virtual Product Product { get; set; }
        public virtual Tenant Tenant { get; set; }

        decimal IOutgoingProduct.Quantity
        {
            get { return Quantity; }
        }

        Guid? IOutgoingProduct.StockTransactionId
        {
            get { return StockTransactionIdOut; }
            set { StockTransactionIdOut = value; }
        }

        decimal IIncomingProduct.Quantity
        {
            get { return ReturnedQuantity ?? 0; }
        }

        Guid? IIncomingProduct.StockTransactionId
        {
            get { return StockTransactionIdIn; }
            set { StockTransactionIdIn = value; }
        }
    }
}
