// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using System.Linq;
using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;

namespace Dyoub.App.Models.ServiceModel.Catalog
{
    public class PricingTable
    {
        public TenantContext Tenant { get; private set; }
        public int StoreId { get; private set; }
        public bool Exists { get; private set; }

        public PricingTable(TenantContext tenant, int storeId)
        {
            Tenant = tenant;
            StoreId = storeId;
        }

        private async Task<Store> FindStoreAndPrices(int storeId)
        {
            return await Tenant.Stores
                .IncludePrices()
                .WhereId(storeId)
                .SingleOrDefaultAsync();
        }

        private IEnumerable<ProductPrice> CopyPricesOf(IEnumerable<ProductPrice> productPrices)
        {
            foreach (ProductPrice productPrice in productPrices)
            {
                yield return new ProductPrice
                {
                    StoreId = StoreId,
                    ProductId = productPrice.ProductId,
                    UnitPrice = productPrice.UnitPrice
                };
            }
        }

        private IEnumerable<ServicePrice> CopyPricesOf(IEnumerable<ServicePrice> servicePrices)
        {
            foreach (ServicePrice servicePrice in servicePrices)
            {
                yield return new ServicePrice
                {
                    StoreId = StoreId,
                    ServiceId = servicePrice.ServiceId,
                    UnitPrice = servicePrice.UnitPrice
                };
            }
        }

        public async Task<bool> Clear()
        {
            Store store = await FindStoreAndPrices(StoreId);

            if (store == null) return false;

            Tenant.ProductPrices.RemoveRange(store.ProductPrices);
            Tenant.ServicePrices.RemoveRange(store.ServicePrices);

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CopyFrom(int sourceStoreId)
        {
            Store store = await FindStoreAndPrices(StoreId);
            Store sourceStore = await FindStoreAndPrices(sourceStoreId);

            if (store == null || sourceStore == null) return false;

            Tenant.ProductPrices.RemoveRange(store.ProductPrices);
            Tenant.ServicePrices.RemoveRange(store.ServicePrices);

            Tenant.ProductPrices.AddRange(CopyPricesOf(sourceStore.ProductPrices));
            Tenant.ServicePrices.AddRange(CopyPricesOf(sourceStore.ServicePrices));

            await Tenant.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(IEnumerable<ItemPrice> itemPrices)
        {
            Store store = await Tenant.Stores
                .WhereId(StoreId)
                .SingleOrDefaultAsync();

            if (store == null) return false;
            
            ICollection<ProductPrice> oldProductPrices = await Tenant.ProductPrices
                .WhereStoreId(StoreId)
                .WhereProductIdIn(itemPrices.Where(i => i.IsProduct).Select(p => p.Id))
                .ToListAsync();

            ICollection<ServicePrice> oldServicePrices = await Tenant.ServicePrices
                .WhereStoreId(StoreId)
                .WhereServiceIdIn(itemPrices.Where(i => i.IsService).Select(p => p.Id))
                .ToListAsync();

            Tenant.ProductPrices.RemoveRange(oldProductPrices);
            Tenant.ServicePrices.RemoveRange(oldServicePrices);

            IEnumerable<ProductPrice> newProductPrices = itemPrices
                .Where(itemPrice => itemPrice.IsProduct && itemPrice.UnitPrice != null)
                .Select(itemPrice => itemPrice.ToProductPrice());

            IEnumerable<ServicePrice> newServicePrices = itemPrices
                .Where(itemPrice => itemPrice.IsService && itemPrice.UnitPrice != null)
                .Select(itemPrice => itemPrice.ToServicePrice());

            Tenant.ProductPrices.AddRange(newProductPrices);
            Tenant.ServicePrices.AddRange(newServicePrices);

            await Tenant.SaveChangesAsync();

            return true;
        }
    }
}
