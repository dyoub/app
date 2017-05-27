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

        public RentedProductListJson(RentContract rentContract)
        {
            RentContract = rentContract;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = RentContract == null ? null : new
            {
                id = RentContract.Id,
                storeId = RentContract.StoreId,
                confirmed = RentContract.Confirmed,
                returnPending = RentContract.ReturnPending,
                startDate = RentContract.StartDate.Date.ToJson(),
                startTime = RentContract.StartDate.TimeOfDay.ToJson(),
                endDate = RentContract.EndDate.Date.ToJson(),
                endTime = RentContract.EndDate.TimeOfDay.ToJson(),
                dateOfReturn = RentContract.DateOfReturn == null ? null :
                    RentContract.DateOfReturn.Value.Date.ToJson(),
                timeOfReturn = RentContract.DateOfReturn == null ? null :
                    RentContract.DateOfReturn.Value.TimeOfDay.ToJson(),
                totalDays = RentContract.TotalDays,
                productList = RentContract.RentedProducts.Select(rentedProduct => new
                {
                    id = rentedProduct.Product.Id,
                    name = rentedProduct.Product.Name,
                    code = rentedProduct.Product.Code,
                    quantity = rentedProduct.Quantity,
                    returnedQuantity = rentedProduct.ReturnedQuantity,
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
