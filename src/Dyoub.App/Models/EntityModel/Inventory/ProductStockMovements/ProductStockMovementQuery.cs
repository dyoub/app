// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Collections.Generic;
using System.Linq;

namespace Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements
{
    public static class ProductStockMovementQuery
    {
        public static IQueryable<ProductStockMovement> WhereTransactionIdIn(this IQueryable<ProductStockMovement> movements, IEnumerable<Guid> transactionIds)
        {
            return movements.Where(movement => transactionIds.Contains(movement.TransactionId));
        }

        public static IQueryable<ProductStockMovement> WhereProductId(this IQueryable<ProductStockMovement> movements, int productId)
        {
            return movements.Where(movement => movement.ProductId == productId);
        }

        public static IQueryable<ProductStockMovement> WhereStoreId(this IQueryable<ProductStockMovement> movements, int storeId)
        {
            return movements.Where(movement => movement.StoreId == storeId);
        }
    }
}
