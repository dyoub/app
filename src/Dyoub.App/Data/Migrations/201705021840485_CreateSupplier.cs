// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;
    
    public partial class CreateSupplier : DbMigration
    {
        public override void Up()
        {
            CreateTable("Supplier", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                NationalId = t.String(nullable: true, maxLength: 50),
                Email = t.String(nullable: true, maxLength: 80),
                Homepage = t.String(nullable: true, maxLength: 80),
                Facebook = t.String(nullable: true, maxLength: 80),
                Instagram = t.String(nullable: true, maxLength: 80),
                Twitter = t.String(nullable: true, maxLength: 80),
                GooglePlus = t.String(nullable: true, maxLength: 80),
                PhoneNumber = t.String(nullable: true, maxLength: 50),
                AlternativePhoneNumber = t.String(nullable: true, maxLength: 50)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "Supplier",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("Supplier");
        }
    }
}
