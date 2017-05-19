// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.ClosureRequests;
using Dyoub.App.Models.EntityModel.Account.PasswordRecoveries;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Catalog.ItemPrices;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.RentContracts;
using Dyoub.App.Models.EntityModel.Commercial.RentedProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleOrders;
using Dyoub.App.Models.EntityModel.Commercial.SalePayments;
using Dyoub.App.Models.EntityModel.Commercial.SaleProducts;
using Dyoub.App.Models.EntityModel.Commercial.SaleServices;
using Dyoub.App.Models.EntityModel.Financial.FixedExpenses;
using Dyoub.App.Models.EntityModel.Financial.OtherCashActivities;
using Dyoub.App.Models.EntityModel.Financial.PurchaseExpenses;
using Dyoub.App.Models.EntityModel.Financial.SaleIncomes;
using Dyoub.App.Models.EntityModel.Financial.Wallets;
using Dyoub.App.Models.EntityModel.Inventory.ProductStockMovements;
using Dyoub.App.Models.EntityModel.Inventory.PurchasedProducts;
using Dyoub.App.Models.EntityModel.Inventory.PurchaseOrders;
using Dyoub.App.Models.EntityModel.Inventory.PurchasePayments;
using Dyoub.App.Models.EntityModel.Inventory.Suppliers;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Manage.TeamRules;
using Dyoub.App.Models.EntityModel.Manage.Teams;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web.Configuration;

namespace Dyoub.App.Models.EntityModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests { get { return Set<ClosureRequest>(); } }
        public DbSet<Customer> Customers { get { return Set<Customer>(); } }
        public DbSet<FixedExpense> FixedExpenses { get { return Set<FixedExpense>(); } }
        public DbSet<OtherCashActivity> OtherCashActivities { get { return Set<OtherCashActivity>(); } }
        public DbSet<PasswordRecovery> PasswordRecoveries { get { return Set<PasswordRecovery>(); } }
        public DbSet<PaymentMethod> PaymentMethods { get { return Set<PaymentMethod>(); } }
        public DbSet<PaymentMethodFee> PaymentMethodFees { get { return Set<PaymentMethodFee>(); } }
        public DbSet<Product> Products { get { return Set<Product>(); } }
        public DbSet<ProductPrice> ProductPrices { get { return Set<ProductPrice>(); } }
        public DbSet<ProductStockMovement> ProductStockMovements { get { return Set<ProductStockMovement>(); } }
        public DbSet<PurchaseExpense> PurchaseExpenses { get { return Set<PurchaseExpense>(); } }
        public DbSet<PurchasedProduct> PurchasedProducts { get { return Set<PurchasedProduct>(); } }
        public DbSet<PurchaseOrder> PurchaseOrders { get { return Set<PurchaseOrder>(); } }
        public DbSet<PurchasePayment> PurchasePayments { get { return Set<PurchasePayment>(); } }
        public DbSet<RentContract> RentContracts { get { return Set<RentContract>(); } }
        public DbSet<RentedProduct> RentedProducts { get { return Set<RentedProduct>(); } }
        public DbSet<SaleOrder> SaleOrders { get { return Set<SaleOrder>(); } }
        public DbSet<SaleIncome> SaleIncomes { get { return Set<SaleIncome>(); } }
        public DbSet<SalePayment> SalePayments { get { return Set<SalePayment>(); } }
        public DbSet<SaleProduct> SaleProducts { get { return Set<SaleProduct>(); } }
        public DbSet<SaleService> SaleServices { get { return Set<SaleService>(); } }
        public DbSet<Service> Services { get { return Set<Service>(); } }
        public DbSet<ServicePrice> ServicePrices { get { return Set<ServicePrice>(); } }
        public DbSet<Store> Stores { get { return Set<Store>(); } }
        public DbSet<Supplier> Suppliers { get { return Set<Supplier>(); } }
        public DbSet<Team> Teams { get { return Set<Team>(); } }
        public DbSet<TeamMember> TeamMembers { get { return Set<TeamMember>(); } }
        public DbSet<TeamRule> TeamRules { get { return Set<TeamRule>(); } }
        public DbSet<Tenant> Tenants { get { return Set<Tenant>(); } }
        public DbSet<User> Users { get { return Set<User>(); } }
        public DbSet<Wallet> Wallets { get { return Set<Wallet>(); } }

        public IQueryable<ItemPrice> ItemPrices
        {
            get
            {
                IQueryable<ItemPrice> productPrices = ProductPrices.AsItemPrice();
                IQueryable<ItemPrice> servicePrices = ServicePrices.AsItemPrice();

                return productPrices.Concat(servicePrices);
            }
        }

        public ApplicationContext() : this(DefaultConnection()) { }

        public ApplicationContext(DbConnection connection) : base(connection, true)
        {
            Database.SetInitializer<ApplicationContext>(null);

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Configurations.Add(new CustomerMapping());
            modelBuilder.Configurations.Add(new ClosureRequestMapping());
            modelBuilder.Configurations.Add(new PaymentMethodMapping());
            modelBuilder.Configurations.Add(new PaymentMethodFeeMapping());
            modelBuilder.Configurations.Add(new PasswordRecoveryMapping());
            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new ProductPriceMapping());
            modelBuilder.Configurations.Add(new ProductStockMovementMapping());
            modelBuilder.Configurations.Add(new PurchaseExpenseMapping());
            modelBuilder.Configurations.Add(new PurchasedProductMapping());
            modelBuilder.Configurations.Add(new PurchaseOrderMapping());
            modelBuilder.Configurations.Add(new PurchasePaymentMapping());
            modelBuilder.Configurations.Add(new RentContractMapping());
            modelBuilder.Configurations.Add(new RentedProductMapping());
            modelBuilder.Configurations.Add(new SaleOrderMapping());
            modelBuilder.Configurations.Add(new SaleIncomeMapping());
            modelBuilder.Configurations.Add(new SalePaymentMapping());
            modelBuilder.Configurations.Add(new SaleProductMapping());
            modelBuilder.Configurations.Add(new SaleServiceMapping());
            modelBuilder.Configurations.Add(new ServiceMapping());
            modelBuilder.Configurations.Add(new ServicePriceMapping());
            modelBuilder.Configurations.Add(new StoreMapping());
            modelBuilder.Configurations.Add(new SupplierMapping());
            modelBuilder.Configurations.Add(new TeamMapping());
            modelBuilder.Configurations.Add(new TeamMemberMapping());
            modelBuilder.Configurations.Add(new TeamRuleMapping());
            modelBuilder.Configurations.Add(new TenantMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new WalletMapping());
        }
        
        private static DbConnection DefaultConnection()
        {
            string providerName = WebConfigurationManager.AppSettings["Database:Provider"];
            string connectionString = WebConfigurationManager.AppSettings["Database:ConnectionString"];

            DbConnection dbConnection = DbProviderFactories.GetFactory(providerName).CreateConnection();
            dbConnection.ConnectionString = connectionString;

            return dbConnection;
        }
    }
}
