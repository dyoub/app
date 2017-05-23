﻿// Copyright (c) Dyoub Applications. All rights reserved.
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
using Dyoub.Test.Factories.Manage;
using Effort;

namespace Dyoub.Test.Contexts.OrderProcessing.RentContractProcessing
{
    public class ConfirmWithoutStockEnoughContext : TenantContext
    {
        private RentPayment rentPayment;
        private RentedProduct rentedProduct;

        public RentContract RentContract { get; private set; }

        public ConfirmWithoutStockEnoughContext() : base(1, DbConnectionFactory.CreateTransient())
        {
            Tenant tenant = Tenants.Add(TenantFactory.Tenant());
            Store store = Stores.Add(StoreFactory.Store(tenant));
            Product product = Products.Add(ProductFactory.Product(tenant));
            PaymentMethod paymentMethod = PaymentMethods.Add(PaymentMethodFactory.PaymentMethod(tenant));

            RentContract = RentContracts.Add(RentContractFactory.RentContract(store));

            rentedProduct = RentedProducts.Add(RentedProductFactory.RentedProduct(RentContract, product));

            rentPayment = RentPayments.Add(RentPaymentFactory.RentPayment(RentContract, paymentMethod));
            rentPayment.NumberOfInstallments = 2;
            rentPayment.InstallmentValue = rentPayment.Total / rentPayment.NumberOfInstallments;

            SaveChanges();
        }
    }
}
