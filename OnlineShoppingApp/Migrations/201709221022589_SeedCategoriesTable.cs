namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Name) VALUES ('Clothes')");
            Sql("INSERT INTO Categories (Name) VALUES ('Electronics')");
            Sql("INSERT INTO Categories (Name) VALUES ('Games')");
            Sql("INSERT INTO Categories (Name) VALUES ('Garden')");
            Sql("INSERT INTO Categories (Name) VALUES ('Health')");
            Sql("INSERT INTO Categories (Name) VALUES ('Home')");
            Sql("INSERT INTO Categories (Name) VALUES ('Jewellery')");
            Sql("INSERT INTO Categories (Name) VALUES ('Music')");
            Sql("INSERT INTO Categories (Name) VALUES ('Pet Supplies')");
            Sql("INSERT INTO Categories (Name) VALUES ('Shoes')");
            Sql("INSERT INTO Categories (Name) VALUES ('Sport')");
            Sql("INSERT INTO Categories (Name) VALUES ('Toys')");
        }
        
        public override void Down()
        {
        }
    }
}
