// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePurchasePayment : DbMigration
    {
        public override void Up()
        {
            CreateTable("PurchasePayment", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                PurchaseOrderId = t.Int(nullable: false),
                NumberOfInstallments = t.Int(nullable: false),
                InstallmentValue = t.Decimal(nullable: false, precision: 8, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Date = t.DateTime(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "PurchasePayment",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "PurchasePayment",
                dependentColumns: new string[] { "PurchaseOrderId", "TenantId" },
                principalTable: "PurchaseOrder",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );
        }

        public override void Down()
        {
            DropTable("PurchasePayment");
        }
    }
}
