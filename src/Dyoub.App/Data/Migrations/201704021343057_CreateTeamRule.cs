// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateTeamRule : DbMigration
    {
        public override void Up()
        {
            CreateTable("TeamRule", t => new
            {
                TeamId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Scope = t.String(nullable: false, maxLength: 50)
            })
            .PrimaryKey(t => new { t.TeamId, t.TenantId, t.Scope });

            AddForeignKey(
                dependentTable: "TeamRule",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );

            AddForeignKey(
                dependentTable: "TeamRule",
                dependentColumns: new string[] { "TeamId", "TenantId" },
                principalTable: "Team",
                principalColumns: new string[] { "Id", "TenantId" },
                cascadeDelete: false
            );
        }
        
        public override void Down()
        {
            DropTable("TeamRule");
        }
    }
}
