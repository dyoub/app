// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateSaleIncome : DbMigration
    {
        public override void Up()
        {
            CreateTable("SaleIncome", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                SalePaymentId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                PaymentDate = t.DateTime(nullable: false),
                ReceivedDate = t.DateTime(nullable: true),
                AmountReceived = t.Decimal(nullable: false, precision: 10, scale: 2),
                AnticipationDate = t.DateTime(nullable: true),
                AmountAnticipated = t.Decimal(nullable: true, precision: 10, scale: 2)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId })
            .Index(t => t.PaymentDate, unique: false)
            .Index(t => t.ReceivedDate, unique: false)
            .Index(t => t.AnticipationDate, unique: false);

            AddForeignKey(
                dependentTable: "SaleIncome",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SaleIncome",
                dependentColumns: new string[] { "SalePaymentId", "TenantId" },
                principalTable: "SalePayment",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );
        }

        public override void Down()
        {
            DropTable("SaleIncome");
        }
    }
}
