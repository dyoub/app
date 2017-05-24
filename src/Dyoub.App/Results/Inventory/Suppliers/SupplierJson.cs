// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.Suppliers
{
    public class SupplierJson : JsonResult
    {
        public Supplier Supplier { get; private set; }

        public SupplierJson(Supplier supplier)
        {
            Supplier = supplier;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Supplier != null)
            {
                Data = new
                {
                    id = Supplier.Id,
                    name = Supplier.Name,
                    nationalId = Supplier.NationalId,
                    email = Supplier.Email,
                    phoneNumber = Supplier.PhoneNumber,
                    alternativePhoneNumber = Supplier.AlternativePhoneNumber,
                    homepage = Supplier.Homepage,
                    facebook = Supplier.Facebook,
                    twitter = Supplier.Twitter,
                    instagram = Supplier.Instagram,
                    googlePlus = Supplier.GooglePlus
                };
            }

            base.ExecuteResult(context);
        }
    }
}
