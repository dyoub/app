// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Extensions;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.RentedProducts
{
    public class RentedProductListJson : JsonResult
    {
        public RentContract RentContract { get; private set; }
        public IEnumerable<RentedProduct> RentedProducts { get; private set; }

        public RentedProductListJson(RentContract rentContract, IEnumerable<RentedProduct> rentedProducts)
        {
            RentContract = rentContract;
            RentedProducts = rentedProducts;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                storeId = RentContract.StoreId,
                confirmed = RentContract.Confirmed,
                startDate = RentContract.StartDate.ToJson(),
                endDate = RentContract.EndDate.ToJson(),
                totalDays = RentContract.TotalDays,
                productList = RentedProducts.Select(rentedProduct => new
                {
                    id = rentedProduct.Product.Id,
                    name = rentedProduct.Product.Name,
                    code = rentedProduct.Product.Code,
                    quantity = rentedProduct.Quantity,
                    unitPrice = rentedProduct.UnitPrice,
                    total = rentedProduct.Total,
                    discount = rentedProduct.Discount,
                    totalPayable = rentedProduct.TotalPayable
                })
            };

            base.ExecuteResult(context);
        }
    }
}
