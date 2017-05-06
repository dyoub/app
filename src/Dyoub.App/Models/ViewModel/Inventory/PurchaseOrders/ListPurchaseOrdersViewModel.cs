// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Filters;
using System;

namespace Dyoub.App.Models.ViewModel.Inventory.PurchaseOrders
{
    public class ListPurchaseOrdersViewModel
    {
        public int? StoreId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [ValidIf]
        public bool StartLowerThanEnd
        {
            get
            {
                return ToDate == null || FromDate.Value < ToDate.Value;
            }
        }

        public int Index { get; set; }
    }
}
