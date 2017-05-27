// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateRentPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("ProductPrice", "UnitRentPrice", t => t.Decimal(
                nullable: true,
                precision: 8,
                scale: 2
            ));

            AlterColumn("ProductPrice", "UnitPrice", t => t.Decimal(
                nullable: true,
                precision: 8,
                scale: 2
            ));

            RenameColumn("ProductPrice", "UnitPrice", "UnitSalePrice");
        }

        public override void Down()
        {
            DropColumn("ProductPrice", "UnitRentPrice");
            RenameColumn("ProductPrice", "UnitSalePrice", "UnitPrice");
        }
    }
}
