// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateStore : DbMigration
    {
        public override void Up()
        {
            CreateTable("Store", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Active = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => t.Id, "PK_Store")
            .ForeignKey("Tenant", t => new { t.TenantId }, false, "FK_Store_Tenant");
        }
        
        public override void Down()
        {
            DropTable("Store");
        }
    }
}