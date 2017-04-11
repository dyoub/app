// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using Dyoub.App.Models.EntityModel.Account;
using Dyoub.App.Models.EntityModel.Catalog;
using Dyoub.App.Models.EntityModel.Manage;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Configuration;

namespace Dyoub.App.Models.EntityModel
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClosureRequest> ClosureRequests { get; set; }
        public DbSet<PasswordRecovery> PasswordRecoveries { get; set; }
        public DbSet<Product> Products { get; set; }
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

            modelBuilder.Configurations.Add(new ClosureRequestMapping());
            modelBuilder.Configurations.Add(new PasswordRecoveryMapping());
            modelBuilder.Configurations.Add(new ProductMapping());
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
