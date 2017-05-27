// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Models.EntityModel.Inventory
{
    public interface IOutgoingProduct
    {
        int ProductId { get; }
        decimal Quantity { get; }
        Guid? StockTransactionId { get; set; }
    }
}
