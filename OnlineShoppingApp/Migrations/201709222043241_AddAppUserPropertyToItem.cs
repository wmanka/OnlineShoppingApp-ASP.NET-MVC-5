namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAppUserPropertyToItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Items", "User_Id");
            AddForeignKey("dbo.Items", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Items", new[] { "User_Id" });
            DropColumn("dbo.Items", "User_Id");
        }
    }
}
