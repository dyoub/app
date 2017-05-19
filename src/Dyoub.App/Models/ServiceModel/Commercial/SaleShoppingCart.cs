// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using Dyoub.App.Models.EntityModel.Commercial.SaleItems;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;
using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Dyoub.App.Models.ServiceModel.Commercial
{
    public class SaleShoppingCart
    {
        public TenantContext Tenant { get; private set; }
        public SaleOrder SaleOrder { get; private set; }
        public bool HasOneOrMoreItemsNotFound { get; private set; }
        public bool HasOneOrMorePricesNotDefined { get; private set; }
        public bool HasItemWithTotalNegative { get; private set; }

        public SaleShoppingCart(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private IEnumerable<SaleProduct> SaleProducts(IEnumerable<SaleItem> itemList)
        {
            return itemList.Where(item => item.IsProduct).Select(item => new SaleProduct
            {
                SaleOrderId = SaleOrder.Id,
                ProductId = item.Id,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = item.Total,
                Discount = item.Discount,
                TotalPayable = item.TotalPayable
            });
        }

        private IEnumerable<SaleService> SaleServices(IEnumerable<SaleItem> itemList)
        {
            return itemList.Where(item => item.IsService).Select(item => new SaleService
            {
                SaleOrderId = SaleOrder.Id,
                ServiceId = item.Id,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                Total = item.Total,
                Discount = item.Discount,
                TotalPayable = item.TotalPayable
            });
        }

        private bool CanFractionOf(SaleItem item, IEnumerable<ItemPrice> priceList)
        {
            ItemPrice itemPrice = priceList.Single(price =>
                price.ItemId == item.Id &&
                price.IsProduct == item.IsProduct &&
                price.IsService == item.IsService);

            return itemPrice.CanFraction;
        }

        private decimal PriceOf(SaleItem item, IEnumerable<ItemPrice> priceList)
        {
            ItemPrice itemPrice = priceList.Single(price =>
                price.ItemId == item.Id &&
                price.IsProduct == item.IsProduct &&
                price.IsService == item.IsService);

            return itemPrice.UnitSalePrice.Value;
        }

        private async Task FindSaleOrder(int saleOrderId)
        {
            SaleOrder = await Tenant.SaleOrders
                .WhereId(saleOrderId)
                .IncludeSaleProducts()
                .IncludeSaleServices()
                .SingleOrDefaultAsync();
        }

        private async Task<IEnumerable<ItemPrice>> CheckPriceListFor(IEnumerable<SaleItem> itemList)
        {
            IEnumerable<ItemPrice> priceList = await Tenant.ItemPrices
                .WhereStoreId(SaleOrder.StoreId)
                .ForSaleItems(itemList)
                .WhereItemMarketed()
                .ToListAsync();

            HasOneOrMoreItemsNotFound = priceList.Count() != itemList.Count();
            HasOneOrMorePricesNotDefined = !priceList.Any() ||
                priceList.Any(itemPrice => itemPrice.UnitSalePrice == null);

            return priceList;
        }

        private void CalculateTotals(IEnumerable<SaleItem> itemList, IEnumerable<ItemPrice> priceList)
        {
            foreach (SaleItem item in itemList)
            {
                if (!CanFractionOf(item, priceList))
                {
                    item.Quantity = (int)item.Quantity;
                }

                item.UnitPrice = PriceOf(item, priceList);
                item.Total = new Money(item.UnitPrice).Multiply(item.Quantity);
                item.TotalPayable = new Money(item.Total).Subtract(item.Discount ?? 0);

                HasItemWithTotalNegative = item.TotalPayable < 0;

                if (HasItemWithTotalNegative) return;
            }

            SaleOrder.Total = new Money(itemList.Sum(item => item.TotalPayable));
            SaleOrder.TotalPayable = new Money(SaleOrder.Total).SubtractPercentage(SaleOrder.Discount ?? 0);
        }

        private async Task Save(IEnumerable<SaleItem> itemList)
        {
            Tenant.SaleProducts.RemoveRange(SaleOrder.SaleProducts);
            Tenant.SaleServices.RemoveRange(SaleOrder.SaleServices);
            Tenant.SaleProducts.AddRange(SaleProducts(itemList));
            Tenant.SaleServices.AddRange(SaleServices(itemList));

            await Tenant.SaveChangesAsync();
        }

        public async Task<bool> Checkout(int saleOrderId, IEnumerable<SaleItem> itemList)
        {
            await FindSaleOrder(saleOrderId);

            if (SaleOrder == null || SaleOrder.Confirmed)
            {
                return false;
            }

            IEnumerable<ItemPrice> priceList = await CheckPriceListFor(itemList);

            if (HasOneOrMoreItemsNotFound || HasOneOrMorePricesNotDefined)
            {
                return false;
            }

            CalculateTotals(itemList, priceList);

            if (HasItemWithTotalNegative)
            {
                return false;
            }

            await Save(itemList);

            return true;
        }
    }
}
