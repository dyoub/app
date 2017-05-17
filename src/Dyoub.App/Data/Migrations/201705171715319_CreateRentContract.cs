// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateRentContract : DbMigration
    {
        public override void Up()
        {
            CreateTable("RentContract", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                StoreId = t.Int(nullable: false),
                WalletId = t.Int(nullable: true),
                CustomerId = t.Int(nullable: true),
                CreatedAt = t.DateTime(nullable: false),
                StartDate = t.DateTime(nullable: false),
                EndDate = t.DateTime(nullable: false),
                ConfirmationDate = t.DateTime(nullable: true),
                DateOfReturn = t.DateTime(nullable: true),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: true, precision: 10, scale: 2),
                TotalPayable = t.Decimal(nullable: false, precision: 10, scale: 2),
                BilledAmount = t.Decimal(nullable: false, precision: 10, scale: 2),
                Author = t.String(nullable: false, maxLength: 80),
                Location = t.String(nullable: true, maxLength: 80),
                AdditionalInformation = t.String(nullable: true, maxLength: 255)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId })
            .Index(t => t.StartDate, unique: false)
            .Index(t => t.EndDate, unique: false);

            AddForeignKey(
                dependentTable: "RentContract",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "RentContract",
                dependentColumns: new string[] { "StoreId", "TenantId" },
                principalTable: "Store",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "RentContract",
                dependentColumns: new string[] { "CustomerId", "TenantId" },
                principalTable: "Customer",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "RentContract",
                dependentColumns: new string[] { "WalletId", "TenantId" },
                principalTable: "Wallet",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("RentContract");
        }
    }
}
