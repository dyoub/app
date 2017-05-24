// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePartner : DbMigration
    {
        public override void Up()
        {
            CreateTable("Partner", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                NationalId = t.String(nullable: true, maxLength: 50),
                Email = t.String(nullable: true, maxLength: 80),
                PhoneNumber = t.String(nullable: true, maxLength: 50),
                AlternativePhoneNumber = t.String(nullable: true, maxLength: 50),
                AdditionalInformation = t.String(nullable: true, maxLength: 255)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "Partner",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("Partner");
        }
    }
}
