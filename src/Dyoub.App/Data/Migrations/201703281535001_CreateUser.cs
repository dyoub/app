// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUser : DbMigration
    {
        public override void Up()
        {
            CreateTable("User", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Email = t.String(nullable: false, maxLength: 80),
                Salt = t.String(nullable: false, maxLength: 50),
                Password = t.String(nullable: false, maxLength: 255),
                Token = t.String(nullable: true, maxLength: 80),
                LastLogin = t.DateTime(nullable: false),
                LastChangePassword = t.DateTime(nullable: false)
            })
            .PrimaryKey(t => t.Id, name: "PK_User")
            .ForeignKey("Tenant", t => new { t.TenantId })
            .Index(t => t.Email, name: "IX_User_Email", unique: true)
            .Index(t => t.Salt, name: "IX_User_Salt", unique: true);
        }

        public override void Down()
        {
            DropTable("User");
        }
    }
}
