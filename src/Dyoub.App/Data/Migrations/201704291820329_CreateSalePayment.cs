// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateSalePayment : DbMigration
    {
        public override void Up()
        {
            CreateTable("SalePayment", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                SaleOrderId = t.Int(nullable: false),
                PaymentMethodId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                NumberOfInstallments = t.Int(nullable: false),
                InstallmentValue = t.Decimal(nullable: false, precision: 8, scale: 2),
                InstallmentBilling = t.Decimal(nullable: false, precision: 8, scale: 2),
                FeePercentage = t.Decimal(nullable: true, precision: 5, scale: 2),
                FeeFixedValue = t.Decimal(nullable: true, precision: 5, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                BilledAmount = t.Decimal(nullable: false, precision: 10, scale: 2),
                Date = t.DateTime(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "SalePayment",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "SalePayment",
                dependentColumns: new string[] { "SaleOrderId", "TenantId" },
                principalTable: "SaleOrder",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "SalePayment",
                dependentColumns: new string[] { "PaymentMethodId", "TenantId" },
                principalTable: "PaymentMethod",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("SalePayment");
        }
    }
}
