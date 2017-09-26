namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMorePropToOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "HasBeenShipped", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "CustomerCity", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Orders", "CustomerPostalCode", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.Orders", "CustomerCountry", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CustomerCountry");
            DropColumn("dbo.Orders", "CustomerPostalCode");
            DropColumn("dbo.Orders", "CustomerCity");
            DropColumn("dbo.Orders", "HasBeenShipped");
        }
    }
}
