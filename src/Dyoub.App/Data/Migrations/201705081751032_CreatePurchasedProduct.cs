// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePurchasedProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable("PurchasedProduct", t => new
            {
                PurchaseOrderId = t.Int(nullable: false),
                ProductId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Quantity = t.Decimal(nullable: false, precision: 9, scale: 3),
                UnitCost = t.Decimal(nullable: false, precision: 8, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: true, precision: 10, scale: 2),
                TotalCost = t.Decimal(nullable: false, precision: 10, scale: 2)
            })
            .PrimaryKey(t => new { t.PurchaseOrderId, t.ProductId, t.TenantId });

            AddForeignKey(
                dependentTable: "PurchasedProduct",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "PurchasedProduct",
                dependentColumns: new string[] { "ProductId", "TenantId" },
                principalTable: "Product",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "PurchasedProduct",
                dependentColumns: new string[] { "PurchaseOrderId", "TenantId" },
                principalTable: "PurchaseOrder",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );
        }

        public override void Down()
        {
            DropTable("PurchasedProduct");
        }
    }
}
