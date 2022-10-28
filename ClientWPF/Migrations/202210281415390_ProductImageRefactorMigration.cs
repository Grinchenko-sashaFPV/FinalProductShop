namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImageRefactorMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsImages", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductsImages", new[] { "ProductId" });
            DropTable("dbo.ProductsImages");
        }
    }
}
