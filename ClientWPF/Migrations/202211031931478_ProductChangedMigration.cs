namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductChangedMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageBytes", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageBytes");
        }
    }
}
