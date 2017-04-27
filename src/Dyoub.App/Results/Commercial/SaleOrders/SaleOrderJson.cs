// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SaleOrders
{
    public class SaleOrderJson : JsonResult
    {
        public SaleOrder SaleOrder { get; private set; }

        public SaleOrderJson(SaleOrder saleOrder)
        {
            SaleOrder = saleOrder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = new
            {
                id = SaleOrder.Id,
                issueDate = SaleOrder.IssueDate,
                confirmationDate = SaleOrder.ConfirmationDate,
                additionalInformation = SaleOrder.AdditionalInformation,
                total = SaleOrder.Total,
                billedAmount = SaleOrder.BilledAmount,
                confirmed = SaleOrder.Confirmed,
                store = new
                {
                    id = SaleOrder.Store.Id,
                    name = SaleOrder.Store.Name,
                }
            };

            base.ExecuteResult(context);
        }
    }
}
