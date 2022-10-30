namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImageNullableMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductsImages", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductsImages", "Image", c => c.Binary(nullable: false));
        }
    }
}
