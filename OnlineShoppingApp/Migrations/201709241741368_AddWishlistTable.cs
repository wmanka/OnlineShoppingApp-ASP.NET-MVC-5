namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWishlistTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ItemId, t.UserId })
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wishlists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Wishlists", "ItemId", "dbo.Items");
            DropIndex("dbo.Wishlists", new[] { "UserId" });
            DropIndex("dbo.Wishlists", new[] { "ItemId" });
            DropTable("dbo.Wishlists");
        }
    }
}
