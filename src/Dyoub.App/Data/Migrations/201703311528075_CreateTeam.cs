// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable("Team", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Active = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => t.Id, "PK_Team")
            .ForeignKey("Tenant", t => new { t.TenantId }, false, "FK_Team_Tenant");
        }

        public override void Down()
        {
            DropTable("Team");
        }
    }
}
