// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;
    
    public partial class CreateFixedExpense : DbMigration
    {
        public override void Up()
        {
            CreateTable("FixedExpense", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                StoreId = t.Int(nullable: false),
                Description = t.String(nullable: false, maxLength: 80),
                StartDate = t.DateTime(nullable: false),
                EndDate = t.DateTime(nullable: true),
                Value = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId })
            .Index(t => t.StartDate);
            
            AddForeignKey(
                dependentTable: "FixedExpense",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "FixedExpense",
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("FixedExpense");
        }
    }
}
