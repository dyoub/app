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
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumns: new string[] { "ProductId", "TenantId" },
                principalTable: "Product",
                principalColumns: new string[] { "Id", "TenantId" },
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
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumns: new string[] { "ServiceId", "TenantId" },
                principalTable: "Service",
                principalColumns: new string[] { "Id", "TenantId" },
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
