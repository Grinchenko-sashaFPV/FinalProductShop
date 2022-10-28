namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImageMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Popularity", c => c.Double(nullable: false));
            AddColumn("dbo.Producers", "Rate", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.Producers", "ProducerRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Producers", "ProducerRate", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "CreationDate");
            DropColumn("dbo.Producers", "Rate");
            DropColumn("dbo.Categories", "Popularity");
        }
    }
}
