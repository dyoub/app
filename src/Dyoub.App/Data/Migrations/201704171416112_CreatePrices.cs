// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePrices : DbMigration
    {
        public override void Up()
        {
            CreateTable("ProductPrice", t => new
            {
                StoreId = t.Int(nullable: false),
                ProductId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.StoreId, t.ProductId, t.TenantId });

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            CreateTable("ServicePrice", t => new
            {
                StoreId = t.Int(nullable: false),
                ServiceId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.StoreId, t.ServiceId, t.TenantId });

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("ProductPrice");
            DropTable("ServicePrice");
        }
    }
}
