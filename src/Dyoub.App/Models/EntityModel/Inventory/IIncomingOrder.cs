// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections.Generic;

namespace Dyoub.App.Models.EntityModel.Inventory
{
    public interface IIncomingOrder
    {
        int StoreId { get; }
        DateTime Date { get; }

        IEnumerable<IIncomingProduct> IncomingList { get; }
    }
}
