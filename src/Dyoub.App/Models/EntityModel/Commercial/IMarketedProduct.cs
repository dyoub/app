// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;

namespace Dyoub.App.Models.EntityModel.Commercial
{
    public interface IMarketedProduct
    {
        int ProductId { get; }
        decimal Quantity { get; }
        Guid? StockTransactionId { get; set; }
    }
}
