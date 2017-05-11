// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Models.ViewModel.Catalog.Products
{
    public class ListProductsViewModel
    {
        public string NameOrCode { get; set; }
        public bool? StockMovement { get; set; }
        public int Index { get; set; }
        public int? Limit { get; set; }
    }
}
