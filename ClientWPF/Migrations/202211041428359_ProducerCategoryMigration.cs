namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProducerCategoryMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producers", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Producers", "CategoryId");
            AddForeignKey("dbo.Producers", "CategoryId", "dbo.Categories", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Producers", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Producers", new[] { "CategoryId" });
            DropColumn("dbo.Producers", "CategoryId");
        }
    }
}
