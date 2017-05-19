// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Financial;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;

namespace Dyoub.App.Models.ServiceModel.Commercial
{
    public class RentShoppingCart
    {
        public TenantContext Tenant { get; private set; }
        public RentContract RentContract { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductPrice> ProductPrices { get; private set; }
        public bool HasOneOrMoreProductsNotFound { get; private set; }
        public bool HasOneOrMoreProductsNotMarketed { get; private set; }
        public bool HasOneOrMorePricesNotDefined { get; private set; }
        public bool HasProductWithTotalNegative { get; private set; }

        public RentShoppingCart(TenantContext tenant)
        {
            Tenant = tenant;
        }

        private async Task FindRentContract(int rentContractId)
        {
            RentContract = await Tenant.RentContracts
                .WhereId(rentContractId)
                .IncludeRentedProducts()
                .SingleOrDefaultAsync();
        }

        private async Task CheckProductsAndPricesOf(IEnumerable<RentedProduct> rentedProductList)
        {
            Products = await Tenant.Products
                .WhereIdIn(rentedProductList.Select(p => p.ProductId))
                .WhereMarketed(true)
                .IncludePrices()
                .ToListAsync();

            ProductPrices = Products.SelectMany(product => product.ProductPrices);

            HasOneOrMoreProductsNotFound = rentedProductList.Count() != Products.Count();
            HasOneOrMoreProductsNotMarketed = Products.Any(product => !product.Marketed);
            HasOneOrMorePricesNotDefined = !ProductPrices.Any() ||
                ProductPrices.Any(productPrice =>
                    productPrice == null ||
                    productPrice.UnitRentPrice == null);
        }
        
        private bool CanFractionOf(RentedProduct rentedProduct)
        {
            return Products
                .Where(product => product.Id == rentedProduct.ProductId)
                .Single().CanFraction;
        }

        private decimal PriceOf(RentedProduct rentedProduct)
        {
            return ProductPrices.Where(price => price.ProductId == rentedProduct.ProductId)
                .Single().UnitRentPrice.Value;
        }

        private void CalculateTotalsOf(IEnumerable<RentedProduct> rentedProductList)
        {
            foreach (RentedProduct rentedProduct in rentedProductList)
            {
                if (!CanFractionOf(rentedProduct))
                {
                    rentedProduct.Quantity = (int)rentedProduct.Quantity;
                }

                rentedProduct.UnitPrice = PriceOf(rentedProduct);

                rentedProduct.Total = new Money(rentedProduct.UnitPrice)
                    .Multiply(rentedProduct.Quantity)
                    .Multiply(RentContract.TotalDays);

                rentedProduct.TotalPayable = new Money(rentedProduct.Total)
                    .Subtract(rentedProduct.Discount ?? 0);

                HasProductWithTotalNegative = rentedProduct.TotalPayable < 0;

                if (HasProductWithTotalNegative) return;
            }

            RentContract.Total = new Money(rentedProductList.Sum(product => product.TotalPayable));

            RentContract.TotalPayable = new Money(RentContract.Total)
                .SubtractPercentage(RentContract.Discount ?? 0);
        }

        private async Task Save(IEnumerable<RentedProduct> rentedProductList)
        {
            Tenant.RentedProducts.RemoveRange(RentContract.RentedProducts);
            Tenant.RentedProducts.AddRange(rentedProductList);
            await Tenant.SaveChangesAsync();
        }

        public async Task<bool> Checkout(int rentContractId, IEnumerable<RentedProduct> rentedProductList)
        {
            await FindRentContract(rentContractId);

            if (RentContract == null || RentContract.Confirmed)
            {
                return false;
            }

            await CheckProductsAndPricesOf(rentedProductList);
            if (HasOneOrMoreProductsNotFound) return false;
            if (HasOneOrMoreProductsNotMarketed) return false;
            if (HasOneOrMorePricesNotDefined) return false;

            CalculateTotalsOf(rentedProductList);
            if (HasProductWithTotalNegative) return false;

            await Save(rentedProductList);

            return true;
        }
    }
}
