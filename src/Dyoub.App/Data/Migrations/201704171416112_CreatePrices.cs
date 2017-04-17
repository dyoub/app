namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreatePrices : DbMigration
    {
        public override void Up()
        {
            CreateTable("ProductPrice", t => new
            {
                StoreId = t.Int(nullable: false),
                ProductId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.StoreId, t.ProductId, t.TenantId }, "PK_ProductPrice");

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                cascadeDelete: true,
                name: "FK_ProductPrice_Store"
            );

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                cascadeDelete: true,
                name: "FK_ProductPrice_Product"
            );

            AddForeignKey(
                dependentTable: "ProductPrice",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_ProductPrice_Tenant"
            );

            CreateTable("ServicePrice", t => new
            {
                StoreId = t.Int(nullable: false),
                ServiceId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2)
            })
            .PrimaryKey(t => new { t.StoreId, t.ServiceId, t.TenantId }, "PK_ServicePrice");

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                cascadeDelete: true,
                name: "FK_ServicePrice_Store"
            );

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "ServiceId",
                principalTable: "Service",
                principalColumn: "Id",
                cascadeDelete: true,
                name: "FK_ServicePrice_Service"
            );

            AddForeignKey(
                dependentTable: "ServicePrice",
                dependentColumn: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                cascadeDelete: false,
                name: "FK_ServicePrice_Tenant"
            );
        }

        public override void Down()
        {
            DropTable("ProductPrice");
            DropTable("ServicePrice");
        }
    }
}
