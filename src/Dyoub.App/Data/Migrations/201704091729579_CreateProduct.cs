// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable("Product", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Code = t.String(nullable: true, maxLength: 50),
                AdditionalInformation = t.String(nullable: true, maxLength: 255),
                Marketed = t.Boolean(nullable: false),
                IsManufactured = t.Boolean(nullable: false),
                StockMovement = t.Boolean(nullable: false),
                CanFraction = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId }, "PK_Product");
            
            AddForeignKey(
                dependentTable: "Product",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_Product_Tenant"
            );
        }

        public override void Down()
        {
            DropTable("Product");
        }
    }
}
