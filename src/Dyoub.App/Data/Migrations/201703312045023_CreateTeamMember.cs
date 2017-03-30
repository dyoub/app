// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateTeamMember : DbMigration
    {
        public override void Up()
        {
            CreateTable("TeamMember", t => new
            {
                UserId = t.Int(nullable: false),
                TeamId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false)
            })
            .PrimaryKey(t => new { t.UserId, t.TeamId, t.TenantId }, "PK_TeamMember")
            .ForeignKey("Tenant", t => t.TenantId, false, "FK_TeamMember_Tenant")
            .Index(t => t.UserId, "IX_TeamMember_UserId")
            .Index(t => t.TeamId, "IX_TeamMember_TeamId");
            
            AddForeignKey(
                dependentTable: "TeamMember",
                dependentColumns: new string[] { "UserId", "TenantId" },
                principalTable: "User",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false,
                name: "FK_TeamMember_User"
            );

            AddForeignKey(
                dependentTable: "TeamMember",
                dependentColumns: new string[] { "TeamId", "TenantId" },
                principalTable: "Team",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false,
                name: "FK_TeamMember_Team"
            );
        }

        public override void Down()
        {
            DropTable("TeamMember");
        }
    }
}
