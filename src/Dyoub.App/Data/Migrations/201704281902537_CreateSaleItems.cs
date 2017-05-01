// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;
    
    public partial class CreateSaleItems : DbMigration
    {
        public override void Up()
        {
            CreateTable("SaleProduct", t => new
            {
                SaleOrderId = t.Int(nullable: false),
                ProductId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Quantity = t.Decimal(nullable: false, precision: 9, scale: 3),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: false, precision: 10, scale: 2),
                TotalPayable = t.Decimal(nullable: false, precision: 10, scale: 2)
            })
            .PrimaryKey(t => new { t.SaleOrderId, t.ProductId, t.TenantId });

            AddForeignKey(
                dependentTable: "SaleProduct",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleProduct",
                dependentColumns: new string[] { "ProductId", "TenantId" },
                principalTable: "Product",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleProduct",
                dependentColumns: new string[] { "SaleOrderId", "TenantId" },
                principalTable: "SaleOrder",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            CreateTable("SaleService", t => new
            {
                SaleOrderId = t.Int(nullable: false),
                ServiceId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Quantity = t.Decimal(nullable: false, precision: 8, scale: 3),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: false, precision: 10, scale: 2),
                TotalPayable = t.Decimal(nullable: false, precision: 10, scale: 2)
            })
            .PrimaryKey(t => new { t.SaleOrderId, t.ServiceId, t.TenantId });

            AddForeignKey(
                dependentTable: "SaleService",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleService",
                dependentColumns: new string[] { "ServiceId", "TenantId" },
                principalTable: "Service",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleService",
                dependentColumns: new string[] { "SaleOrderId", "TenantId" },
                principalTable: "SaleOrder",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("SaleProduct");
            DropTable("SaleService");
        }
    }
}
