// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePaymentMethod : DbMigration
    {
        public override void Up()
        {
            CreateTable("PaymentMethod", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                InstallmentLimit = t.Int(nullable: false),
                DaysToReceive = t.Int(nullable: true),
                EarlyReceipt = t.Boolean(nullable: false),
                Active = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "PaymentMethod",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            CreateTable("PaymentMethodFee", t => new
            {
                PaymentMethodId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                MinimumInstallment = t.Int(nullable: false),
                FeePercentage = t.Decimal(nullable: true, precision: 5, scale: 2),
                FeeFixedValue = t.Decimal(nullable: true, precision: 5, scale: 2)
            })
            .PrimaryKey(t => new { t.PaymentMethodId, t.TenantId, t.MinimumInstallment });

            AddForeignKey(
                dependentTable: "PaymentMethodFee",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "PaymentMethodFee",
                dependentColumn: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                cascadeDelete: true
            );
        }

        public override void Down()
        {
            DropTable("PaymentMethodFee");
            DropTable("PaymentMethod");
        }
    }
}
