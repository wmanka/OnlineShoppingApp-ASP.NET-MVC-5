namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullPricePropToOderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "FullPrice", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "FullPrice");
        }
    }
}
