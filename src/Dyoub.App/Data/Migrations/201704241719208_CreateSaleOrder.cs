// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateSaleOrder : DbMigration
    {
        public override void Up()
        {
            CreateTable("SaleOrder", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                StoreId = t.Int(nullable: false),
                CustomerId = t.Int(nullable: true),
                IssueDate = t.DateTime(nullable: false),
                ConfirmationDate = t.DateTime(nullable: true),
                CreatedAt = t.DateTime(nullable: false),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: true, precision: 10, scale: 2),
                TotalPayable = t.Decimal(nullable: false, precision: 10, scale: 2),
                BilledAmount = t.Decimal(nullable: false, precision: 10, scale: 2),
                Author = t.String(nullable: false, maxLength: 80),
                AdditionalInformation = t.String(nullable: true, maxLength: 255)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId })
            .Index(t => t.IssueDate, unique: false);

            AddForeignKey(
                dependentTable: "SaleOrder",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleOrder",
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleOrder",
                dependentColumns: new string[] { "CustomerId", "TenantId" },
                principalTable: "Customer",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }
        
        public override void Down()
        {
            DropTable("SaleOrder");
        }
    }
}
