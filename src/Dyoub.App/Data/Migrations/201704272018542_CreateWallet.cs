// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateWallet : DbMigration
    {
        public override void Up()
        {
            CreateTable("Wallet", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                AdditionalInformation = t.String(nullable: true, maxLength: 255),
                Active = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "Wallet",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );
            
            AddColumn("SaleOrder", "WalletId", t => t.Int(nullable: true));

            AddForeignKey(
                dependentTable: "SaleOrder",
                dependentColumn: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id",
                cascadeDelete: false
            );
        }
        
        public override void Down()
        {
            DropForeignKey(
                dependentTable: "SaleOrder",
                dependentColumn: "WalletId",
                principalTable: "Wallet"
            );

            DropColumn("SaleOrder", "WalletId");

            DropTable("Wallet");
        }
    }
}
