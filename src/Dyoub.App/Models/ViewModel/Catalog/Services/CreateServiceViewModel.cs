// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Catalog.Services;
using System.ComponentModel.DataAnnotations;

namespace Dyoub.App.Models.ViewModel.Catalog.Services
{
    public class CreateServiceViewModel
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

        public Service MapTo(Service service)
        {
            service.Name = Name;
            service.Code = Code;
            service.Marketed = Marketed;
            service.CanFraction = CanFraction;
            service.AdditionalInformation = AdditionalInformation;

            return service;
        }
    }
}
