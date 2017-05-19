// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

namespace Dyoub.App.Data
{
    using System.Data.Entity.Migrations;

    public partial class CreateRentedProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable("RentedProduct", t => new
            {
                RentContractId = t.Int(nullable: false),
                ProductId = t.Int(nullable: false),
                TenantId = t.Int(nullable: false),
                Quantity = t.Decimal(nullable: false, precision: 9, scale: 3),
                UnitPrice = t.Decimal(nullable: false, precision: 8, scale: 2),
                Total = t.Decimal(nullable: false, precision: 10, scale: 2),
                Discount = t.Decimal(nullable: true, precision: 10, scale: 2),
                TotalPayable = t.Decimal(nullable: false, precision: 10, scale: 2),
                StockTransactionId = t.String(nullable: true, maxLength: 36)
            })
            .PrimaryKey(t => new { t.RentContractId, t.ProductId, t.TenantId });
        }
        
        public override void Down()
        {
            DropTable("RentedProduct");
        }
    }
}
