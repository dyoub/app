// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateProductStockMovement : DbMigration
    {
        public override void Up()
        {
            CreateTable("ProductStockMovement", t => new
            {
                TransactionId = t.String(nullable: false, maxLength: 36),
                ProductId = t.Int(nullable: false),
                StoreId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Quantity = t.Decimal(nullable: false, precision: 9, scale: 3),
                Date = t.DateTime(nullable: false)
            })
            .PrimaryKey(t => new { t.TransactionId, t.ProductId, t.StoreId, t.TenantId })
            .Index(t => t.Date);

            AddForeignKey(
                dependentTable: "ProductStockMovement",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "ProductStockMovement",
                dependentColumns: new string[] { "ProductId", "TenantId" },
                principalTable: "Product",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "ProductStockMovement",
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );

            AddColumn("PurchasedProduct", "StockTransactionId", t => t.String(nullable: true, maxLength: 36));
            AddColumn("SaleProduct", "StockTransactionId", t => t.String(nullable: true, maxLength: 36));
        }

        public override void Down()
        {
            DropColumn("PurchasedProduct", "StockTransactionId");
            DropColumn("SaleProduct", "StockTransactionId");

            DropTable("ProductStockMovement");
        }
    }
}
