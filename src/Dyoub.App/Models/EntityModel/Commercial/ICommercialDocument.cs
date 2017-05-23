// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Commercial
{
    public interface ICommercialDocument
    {
        int StoreId { get; }
        DateTime Date { get; }
        DateTime? ConfirmationDate { get; set; }
        decimal BilledAmount { get; set; }

        IEnumerable<IPayment> Payments { get; }
        IEnumerable<IMarketedProduct> MarketedProducts { get; }
    }
}
