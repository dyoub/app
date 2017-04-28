namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateService : DbMigration
    {
        public override void Up()
        {
            CreateTable("Service", t => new
            {
                Id = t.Int(nullable: false, identity: true),
                TenantId = t.Int(nullable: false),
                Name = t.String(nullable: false, maxLength: 80),
                Code = t.String(nullable: true, maxLength: 50),
                AdditionalInformation = t.String(nullable: true, maxLength: 255),
                Marketed = t.Boolean(nullable: false),
                CanFraction = t.Boolean(nullable: false)
            })
            .PrimaryKey(t => new { t.Id, t.TenantId });

            AddForeignKey(
                dependentTable: "Service",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false
            );
        }

        public override void Down()
        {
            DropTable("Service");
        }
    }
}
