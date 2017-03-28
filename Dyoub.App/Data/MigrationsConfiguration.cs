// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using Models.DataModel;
    using MySql.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class MigrationsConfiguration : DbMigrationsConfiguration<ApplicationContext>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Data\Migrations";
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
        }

        protected override void Seed(ApplicationContext context)
        {
            // Nothing for now.
        }
    }
}
