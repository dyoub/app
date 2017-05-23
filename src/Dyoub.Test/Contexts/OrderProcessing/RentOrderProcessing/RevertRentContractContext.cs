// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.Test.Factories.Account;
using Dyoub.Test.Factories.Catalog;
using Dyoub.Test.Factories.Commercial;
using Dyoub.Test.Factories.Inventory;
using Dyoub.Test.Factories.Manage;
using Effort;
using System.Linq;

namespace Dyoub.Test.Contexts.OrderProcessing.RentContractProcessing
{
    public class RevertRentContractContext : TenantContext
    {
        private RentPayment rentPayment;
        private RentedProduct rentedProduct;

        public RentContract RentContract { get; private set; }

        public RevertRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            RentContract = RentContracts.Add(RentContractFactory.ConfirmedRentContract(store));

            rentedProduct = RentedProducts.Add(RentedProductFactory.RentedProduct(RentContract, product));
            ProductStockMovements.Add(ProductStockMovementFactory.ProductStockMovement(rentedProduct));

            rentPayment = RentPayments.Add(RentPaymentFactory.RentPayment(RentContract, paymentMethod));
            rentPayment.NumberOfInstallments = 2;

            SaveChanges();
        }

        public bool RentContractHasBeenReverted()
        {
            Entry(RentContract).Reload();

            return !RentContract.Confirmed;
        }

        public bool BillingValuesHaveBeenReset()
        {
            Entry(rentPayment).Reload();

            return RentContract.BilledAmount == 0 &&
                   rentPayment.InstallmentBilling == 0 &&
                   rentPayment.BilledAmount == 0;
        }

        public bool RentIncomesHaveBeenRemoved()
        {
            return !RentIncomes.Any();
        }

        public bool StockTransacionsHaveBeenRemoved()
        {
            Entry(rentedProduct).Reload();

            return rentedProduct.StockTransactionId == null &&
                   !ProductStockMovements.Any();
        }
    }
}
