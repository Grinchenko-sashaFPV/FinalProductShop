namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedProductModelMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ImageBytes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ImageBytes", c => c.Binary());
        }
    }
}
