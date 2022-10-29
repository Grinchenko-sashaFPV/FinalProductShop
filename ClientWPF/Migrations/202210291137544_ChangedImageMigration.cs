namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedImageMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductsImages", "FileExtension", c => c.String());
            AddColumn("dbo.ProductsImages", "Size", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductsImages", "Size");
            DropColumn("dbo.ProductsImages", "FileExtension");
        }
    }
}
