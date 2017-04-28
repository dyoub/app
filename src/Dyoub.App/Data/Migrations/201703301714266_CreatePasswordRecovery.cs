// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePasswordRecovery : DbMigration
    {
        public override void Up()
        {
            CreateTable("PasswordRecovery", t => new
            {
                Token = t.String(nullable: false, maxLength: 50),
                Email = t.String(nullable: false, maxLength: 80),
                RequestDate = t.DateTime(nullable: false),
                ExpiryDate = t.DateTime(nullable: false)
            })
            .PrimaryKey(t => t.Token);
        }

        public override void Down()
        {
            DropTable("PasswordRecovery");
        }
    }
}
