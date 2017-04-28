// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateTenant : DbMigration
    {
        public override void Up()
        {
            CreateTable("Tenant", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                Owner = t.String(nullable: false, maxLength: 80),
                CreatedAt = t.DateTime(nullable: false),
                DeactivatedAt = t.DateTime(nullable: true)
            })
            .PrimaryKey(t => t.Id)
            .Index(t => t.Owner);
        }

        public override void Down()
        {
            DropTable("Tenant");
        }
    }
}
