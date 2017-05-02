// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.Suppliers
{
    public class SupplierListJson : JsonResult
    {
        public IEnumerable<Supplier> Suppliers { get; private set; }

        public SupplierListJson(IEnumerable<Supplier> suppliers)
        {
            Suppliers = suppliers;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = Suppliers.Select(supplier => new
            {
                id = supplier.Id,
                name = supplier.Name,
                nationalId = supplier.NationalId,
                email = supplier.Email,
                phoneNumber = supplier.PhoneNumber,
                alternativePhoneNumber = supplier.AlternativePhoneNumber,
                homepage = supplier.Homepage,
                facebook = supplier.Facebook,
                twitter = supplier.Twitter,
                instagram = supplier.Instagram,
                googlePlus = supplier.GooglePlus
            });

            base.ExecuteResult(context);
        }
    }
}
