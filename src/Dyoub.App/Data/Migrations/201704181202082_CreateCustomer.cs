// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable("Customer", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Email = t.String(nullable: true, maxLength: 80),
                NationalId = t.String(nullable: true, maxLength: 50),
                PhoneNumber = t.String(nullable: true, maxLength: 50),
                AlternativePhoneNumber = t.String(nullable: true, maxLength: 50)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId }, "PK_Customer");

            AddForeignKey(
                dependentTable: "Customer",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_Customer_Tenant"
            );
        }

        public override void Down()
        {
            DropTable("Customer");
        }
    }
}
