// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePurchaseExpense : DbMigration
    {
        public override void Up()
        {
            CreateTable("PurchaseExpense", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                PurchasePaymentId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                PaymentDate = t.DateTime(nullable: false),
                AmountPaid = t.Decimal(nullable: false, precision: 10, scale: 2)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId })
            .Index(t => t.PaymentDate, unique: false);

            AddForeignKey(
                dependentTable: "PurchaseExpense",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "PurchaseExpense",
                dependentColumns: new string[] { "PurchasePaymentId", "TenantId" },
                principalTable: "PurchasePayment",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );
        }

        public override void Down()
        {
            DropTable("PurchaseExpense");
        }
    }
}
