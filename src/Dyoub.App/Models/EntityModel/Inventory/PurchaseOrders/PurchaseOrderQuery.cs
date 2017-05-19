// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Data.Entity;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders
{
    public static class PurchaseOrderQuery
    {
        public static IQueryable<PurchaseOrder> IncludePurchaseExpenses(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.PurchasePayments
                .Select(purchasePayment => purchasePayment.PurchaseExpenses));
        }

        public static IQueryable<PurchaseOrder> IncludePurchasePayments(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.PurchasePayments);
        }

        public static IQueryable<PurchaseOrder> IncludePurchasedProducts(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.PurchasedProducts);
        }

        public static IQueryable<PurchaseOrder> IncludeStore(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.Store);
        }

        public static IQueryable<PurchaseOrder> IncludeSupplier(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.Supplier);
        }

        public static IQueryable<PurchaseOrder> IncludeWallet(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.Include(purchaseOrder => purchaseOrder.Wallet);
        }

        public static IQueryable<PurchaseOrder> IssuedOnDate(this IQueryable<PurchaseOrder> purchaseOrders, DateTime? date)
        {
            if (date == null)
            {
                return purchaseOrders;
            }

            date = date.Value.Date;

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.IssueDate == date);
        }

        public static IQueryable<PurchaseOrder> IssuedThisMonth(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            DateTime startDate = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month, 1);
            DateTime endDate = new DateTime(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month,
                DateTime.DaysInMonth(DateTime.UtcNow.Date.Year, DateTime.UtcNow.Date.Month));

            return purchaseOrders.WhereIssueDateStartAt(startDate).WhereIssueDateEndAt(endDate);
        }

        public static IQueryable<PurchaseOrder> IssuedToday(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.IssuedOnDate(DateTime.UtcNow.Date);
        }

        public static IQueryable<PurchaseOrder> OrderByMostRecent(this IQueryable<PurchaseOrder> purchaseOrders)
        {
            return purchaseOrders.OrderByDescending(purchaseOrder => purchaseOrder.IssueDate);
        }

        public static IQueryable<PurchaseOrder> WhereSupplierId(this IQueryable<PurchaseOrder> purchaseOrders, int? supplierId)
        {
            if (supplierId == null)
            {
                return purchaseOrders;
            }

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.SupplierId == supplierId);
        }

        public static IQueryable<PurchaseOrder> WhereId(this IQueryable<PurchaseOrder> purchaseOrders, int id)
        {
            return purchaseOrders.Where(purchaseOrder => purchaseOrder.Id == id);
        }

        public static IQueryable<PurchaseOrder> WhereIssueDateStartAt(this IQueryable<PurchaseOrder> purchaseOrders, DateTime? startDate)
        {
            if (startDate == null)
            {
                return purchaseOrders;
            }

            startDate = startDate.Value.Date;

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.IssueDate >= startDate);
        }

        public static IQueryable<PurchaseOrder> WhereIssueDateEndAt(this IQueryable<PurchaseOrder> purchaseOrders, DateTime? endDate)
        {
            if (endDate == null)
            {
                return purchaseOrders;
            }

            endDate = endDate.Value.Date.AddDays(1);

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.IssueDate < endDate);
        }

        public static IQueryable<PurchaseOrder> WhereStoreId(this IQueryable<PurchaseOrder> purchaseOrders, int? storeId)
        {
            if (storeId == null)
            {
                return purchaseOrders;
            }

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.StoreId == storeId);
        }

        public static IQueryable<PurchaseOrder> WhereWalletId(this IQueryable<PurchaseOrder> purchaseOrders, int? walletId)
        {
            if (walletId == null)
            {
                return purchaseOrders;
            }

            return purchaseOrders.Where(purchaseOrder => purchaseOrder.WalletId == walletId);
        }
    }
}
