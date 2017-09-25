namespace OnlineShoppingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedPaymentTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO PaymentTypes (Name) VALUES ('Visa')");
            Sql("INSERT INTO PaymentTypes (Name) VALUES ('MasterCard')");
            Sql("INSERT INTO PaymentTypes (Name) VALUES ('PayPal')");
        }
        
        public override void Down()
        {
        }
    }
}
