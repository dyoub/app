// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.Products
{
    public class CreateProductViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }
        
        [MaxLength(255)]
        public string AdditionalInformation { get; set; }

        public bool Marketed { get; set; }

        public bool IsManufactured { get; set; }

        public bool StockMovement { get; set; }

        public bool CanFraction { get; set; }

        public Product MapTo(Product product)
        {
            product.Name = Name;
            product.Code = Code;
            product.Marketed = Marketed;
            product.IsManufactured = IsManufactured;
            product.StockMovement = StockMovement;
            product.CanFraction = CanFraction;
            product.AdditionalInformation = AdditionalInformation;

            return product;
        }
    }
}
