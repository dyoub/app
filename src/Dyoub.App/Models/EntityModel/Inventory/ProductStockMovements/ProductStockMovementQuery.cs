// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Inventory.ProductQuantities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements
{
    public static class ProductStockMovementQuery
    {
        public static IQueryable<ProductQuantity> AsProductQuantity(this IQueryable<ProductStockMovement> movements)
        {
            return movements
                .GroupBy(movement => movement.ProductId)
                .Select(productGroup => new ProductQuantity
                {
                    Id = productGroup.FirstOrDefault().Product.Id,
                    Code = productGroup.FirstOrDefault().Product.Code,
                    Name = productGroup.FirstOrDefault().Product.Name,
                    Marketed = productGroup.FirstOrDefault().Product.Marketed,
                    TotalAvailable = productGroup.Sum(product => product.Quantity)
                });
        }

        public static IQueryable<ProductStockMovement> WhereProductId(this IQueryable<ProductStockMovement> movements, int? productId)
        {
            if (productId == null)
            {
                return movements;
            }

            return movements.Where(movement => movement.ProductId == productId);
        }

        public static IQueryable<ProductStockMovement> WhereProductIdIn(this IQueryable<ProductStockMovement> movements, IEnumerable<int> productIds)
        {
            return movements.Where(movement => productIds.Contains(movement.ProductId));
        }

        public static IQueryable<ProductStockMovement> WhereStoreId(this IQueryable<ProductStockMovement> movements, int? storeId)
        {
            if (storeId == null)
            {
                return movements;
            }

            return movements.Where(movement => movement.StoreId == storeId);
        }

        public static IQueryable<ProductStockMovement> WhereTransactionIdIn(this IQueryable<ProductStockMovement> movements, IEnumerable<Guid> transactionIds)
        {
            return movements.Where(movement => transactionIds.Contains(movement.TransactionId));
        }

        public static IQueryable<ProductStockMovement> WhereUntilDate(this IQueryable<ProductStockMovement> movements, DateTime date)
        {
            date = date.Date;
            return movements.Where(movement => movement.Date <= date);
        }
    }
}
