// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentPayments;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Financial.RentIncomes;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
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
    public class ConfirmRentContractContext : TenantContext
    {
        private RentPayment rentPayment;
        private RentedProduct rentedProduct;

        public RentContract RentContract { get; private set; }

        public ConfirmRentContractContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));
            PurchaseOrder purchaseOrder = PurchaseOrders.Add(PurchaseOrderFactory.ConfirmedPurchaseOrder(store));
            PurchasedProduct purchasedProduct = PurchasedProducts.Add(PurchasedProductFactory.PurchasedProduct(purchaseOrder, product));

            ProductStockMovements.Add(ProductStockMovementFactory.ProductStockMovement(purchasedProduct));

            RentContract = RentContracts.Add(RentContractFactory.RentContract(store));

            rentedProduct = RentedProducts.Add(RentedProductFactory.RentedProduct(RentContract, product));

            rentPayment = RentPayments.Add(RentPaymentFactory.RentPayment(RentContract, paymentMethod));
            rentPayment.NumberOfInstallments = 2;
            rentPayment.InstallmentValue = rentPayment.Total / rentPayment.NumberOfInstallments;

            SaveChanges();
        }

        public bool RentContractHasBeenConfirmed()
        {
            Entry(RentContract).Reload();

            return RentContract.Confirmed;
        }

        public bool BillingValuesHaveBeenCalculated()
        {
            return RentContract.BilledAmount == rentPayment.BilledAmount;
        }

        public bool RentIncomesHaveBeenGenerated()
        {
            RentIncome[] incomes = RentIncomes.ToArray();

            return incomes[0].PaymentDate == rentPayment.Date &&
                   incomes[0].AmountReceived == rentPayment.InstallmentValue &&
                   incomes[1].PaymentDate == rentPayment.Date.AddMonths(1) &&
                   incomes[1].AmountReceived == rentPayment.InstallmentValue;
        }

        public bool StockMovementHasBeenRegistered()
        {
            Entry(rentedProduct).Reload();

            return ProductStockMovements
                .Where(movement => movement.TransactionId == rentedProduct.StockTransactionIdOut.Value)
                .Any();
        }
    }
}
