// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateRentContractPartner : DbMigration
    {
        public override void Up()
        {
            CreateTable("RentContractPartner", t => new
            {
                RentContractId = t.Int(nullable: false),
                PartnerId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false)
            })
            .PrimaryKey(t => new { t.RentContractId, t.PartnerId, t.TenantId });

            AddForeignKey(
                dependentTable: "RentContractPartner",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "RentContractPartner",
                dependentColumns: new string[] { "RentContractId", "TenantId" },
                principalTable: "RentContract",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: true
            );

            AddForeignKey(
                dependentTable: "RentContractPartner",
                dependentColumns: new string[] { "PartnerId", "TenantId" },
                principalTable: "Partner",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("RentContractPartner");
        }
    }
}
