// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateOtherCashActivity : DbMigration
    {
        public override void Up()
        {
            CreateTable("OtherCashActivity", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                StoreId = t.Int(nullable: false),
                Description = t.String(nullable: false, maxLength: 80),
                Date = t.DateTime(nullable: false),
                Value = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId }, "PK_OtherCashActivity")
            .Index(t => t.Date, name: "IX_OtherCashActivity_Date");

            AddForeignKey(
                dependentTable: "OtherCashActivity",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_OtherCashActivity_Tenant"
            );

            AddForeignKey(
                dependentTable: "OtherCashActivity",
                dependentColumn: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_OtherCashActivity_Store"
            );
        }

        public override void Down()
        {
            DropTable("OtherCashActivity");
        }
    }
}
