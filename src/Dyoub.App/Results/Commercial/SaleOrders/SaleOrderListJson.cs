// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Commercial.SaleOrders
{
    public class SaleOrderListJson : JsonResult
    {
        public IEnumerable<SaleOrder> SaleOrders { get; private set; }

        public IEnumerable<IGrouping<DateTime, SaleOrder>> SaleOrderHistory
        {
            get
            {
                return SaleOrders.GroupBy(saleOrder => saleOrder.IssueDate);
            }
        }

        public SaleOrderListJson(IEnumerable<SaleOrder> saleOrders)
        {
            SaleOrders = saleOrders;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = SaleOrderHistory.Select(history => new
            {
                date = history.Key,
                saleOrderList = history.Select(saleOrder => new
                {
                    id = saleOrder.Id,
                    issueDate = saleOrder.IssueDate,
                    store = saleOrder.Store.Name,
                    customer = saleOrder.Customer != null ? saleOrder.Customer.Name : null,
                    draft = saleOrder.Draft,
                    budget = saleOrder.Budget
                })
            });

            base.ExecuteResult(context);
        }
    }
}
