// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account.ClosureRequests;
using Dyoub.App.Models.EntityModel.Account.PasswordRecoveries;
using Dyoub.App.Models.EntityModel.Account.Tenants;
using Dyoub.App.Models.EntityModel.Account.Users;
using Dyoub.App.Models.EntityModel.Catalog.ProductPrices;
using Dyoub.App.Models.EntityModel.Catalog.Products;
using Dyoub.App.Models.EntityModel.Catalog.ServicePrices;
using Dyoub.App.Models.EntityModel.Catalog.Services;
using Dyoub.App.Models.EntityModel.Manage.Stores;
using Dyoub.App.Models.EntityModel.Manage.TeamMembers;
using Dyoub.App.Models.EntityModel.Manage.TeamRules;
using Dyoub.App.Models.EntityModel.Manage.Teams;
using Dyoub.App.Models.EntityModel.Commercial.Customers;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Configuration;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethods;
using Dyoub.App.Models.EntityModel.Commercial.PaymentMethodFees;

namespace Dyoub.App.Models.EntityModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PaymentMethodFee> PaymentMethodFees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServicePrice> ServicePrices { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<TeamRule> TeamRules { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }

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
            modelBuilder.Configurations.Add(new ServiceMapping());
            modelBuilder.Configurations.Add(new ServicePriceMapping());
            modelBuilder.Configurations.Add(new StoreMapping());
            modelBuilder.Configurations.Add(new TeamMapping());
            modelBuilder.Configurations.Add(new TeamMemberMapping());
            modelBuilder.Configurations.Add(new TeamRuleMapping());
            modelBuilder.Configurations.Add(new TenantMapping());
            modelBuilder.Configurations.Add(new UserMapping());
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
