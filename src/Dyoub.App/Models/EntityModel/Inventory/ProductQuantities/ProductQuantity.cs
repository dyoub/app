// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Models.EntityModel.Inventory.ProductQuantities
{
    public class ProductQuantity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal TotalAvailable { get; set; }
    }
}
