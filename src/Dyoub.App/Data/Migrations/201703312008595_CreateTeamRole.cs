// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateTeamRole : DbMigration
    {
        public override void Up()
        {
            CreateTable("TeamRole", t => new
            {
                TeamId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Role = t.String(nullable: false, maxLength: 50),
                CanEdit = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => new { t.TeamId, t.TenantId, t.Role }, "PK_TeamRole")
            .ForeignKey("Tenant", t => t.TenantId, false, "FK_TeamRole_Tenant");
            
            AddForeignKey(
                dependentTable: "TeamRole",
                dependentColumns: new string[] { "TeamId", "TenantId" },
                principalTable: "Team",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false,
                name: "FK_TeamRole_Team"
            );
        }

        public override void Down()
        {
            DropTable("TeamRole");
        }
    }
}
