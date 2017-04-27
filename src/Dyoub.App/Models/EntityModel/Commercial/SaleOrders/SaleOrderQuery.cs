// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Commercial.SaleOrders
{
    public static class SaleOrderQuery
    {
        public static IQueryable<SaleOrder> IncludeCustomer(this IQueryable<SaleOrder> saleOrders)
        {
            return saleOrders.Include(saleOrder => saleOrder.Customer);
        }
        
        public static IQueryable<SaleOrder> IncludeStore(this IQueryable<SaleOrder> saleOrders)
        {
            return saleOrders.Include(saleOrder => saleOrder.Store);
        }

        public static IQueryable<SaleOrder> IssuedOnDate(this IQueryable<SaleOrder> saleOrders, DateTime? date)
        {
            if (date == null)
            {
                return saleOrders;
            }

            date = date.Value.Date;

            return saleOrders.Where(saleOrder => saleOrder.IssueDate == date);
        }

        public static IQueryable<SaleOrder> IssuedToday(this IQueryable<SaleOrder> saleOrders)
        {
            return saleOrders.IssuedOnDate(DateTime.Today);
        }

        public static IQueryable<SaleOrder> OrderByMostRecent(this IQueryable<SaleOrder> saleOrders)
        {
            return saleOrders.OrderByDescending(saleOrder => saleOrder.IssueDate);
        }

        public static IQueryable<SaleOrder> WhereCustomerId(this IQueryable<SaleOrder> saleOrders, int? customerId)
        {
            if (customerId == null)
            {
                return saleOrders;
            }

            return saleOrders.Where(saleOrder => saleOrder.CustomerId == customerId);
        }

        public static IQueryable<SaleOrder> WhereId(this IQueryable<SaleOrder> saleOrders, int id)
        {
            return saleOrders.Where(saleOrder => saleOrder.Id == id);
        }

        public static IQueryable<SaleOrder> WhereIssueDateStartAt(this IQueryable<SaleOrder> saleOrders, DateTime? startDate)
        {
            if (startDate == null)
            {
                return saleOrders;
            }

            startDate = startDate.Value.Date;

            return saleOrders.Where(saleOrder => saleOrder.IssueDate >= startDate);
        }

        public static IQueryable<SaleOrder> WhereIssueDateEndAt(this IQueryable<SaleOrder> saleOrders, DateTime? endDate)
        {
            if (endDate == null)
            {
                return saleOrders;
            }

            endDate = endDate.Value.Date.AddDays(1);

            return saleOrders.Where(saleOrder => saleOrder.IssueDate < endDate);
        }

        public static IQueryable<SaleOrder> WhereStoreId(this IQueryable<SaleOrder> saleOrders, int? storeId)
        {
            if (storeId == null)
            {
                return saleOrders;
            }

            return saleOrders.Where(saleOrder => saleOrder.StoreId == storeId);
        }
    }
}
