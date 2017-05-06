// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Dyoub.App.Results.Inventory.PurchaseOrders
{
    public class PurchaseOrderListJson : JsonResult
    {
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; private set; }

        public IEnumerable<IGrouping<DateTime, PurchaseOrder>> PurchaseOrderHistory
        {
            get
            {
                return PurchaseOrders.GroupBy(purchaseOrder => purchaseOrder.IssueDate);
            }
        }

        public PurchaseOrderListJson(IEnumerable<PurchaseOrder> purchaseOrders)
        {
            PurchaseOrders = purchaseOrders;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Data = PurchaseOrderHistory.Select(history => new
            {
                date = history.Key,
                purchaseOrderList = history.Select(purchaseOrder => new
                {
                    id = purchaseOrder.Id,
                    issueDate = purchaseOrder.IssueDate,
                    store = purchaseOrder.Store.Name,
                    supplier = purchaseOrder.Supplier != null ? purchaseOrder.Supplier.Name : null,
                    draft = purchaseOrder.Draft,
                    budget = purchaseOrder.Budget
                })
            });

            base.ExecuteResult(context);
        }
    }
}
