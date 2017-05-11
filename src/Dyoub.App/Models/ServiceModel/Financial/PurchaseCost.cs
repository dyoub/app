// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Financial
{
    public class PurchaseCost
    {
        public async Task<bool> Confirm(int purchaseOrderId)
        {
            throw new NotImplementedException("TODO");
        }

        public async Task<bool> Revert(int purchaseOrderId)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
