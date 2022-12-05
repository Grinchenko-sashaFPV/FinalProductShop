namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRelationShipsMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersImages", "UserId", "dbo.Users");
            DropIndex("dbo.BankAccounts", new[] { "UserId" });
            DropIndex("dbo.UsersImages", new[] { "UserId" });
            DropTable("dbo.BankAccounts");
            DropTable("dbo.UsersImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        FileExtension = c.String(),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoneyAmount = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UsersImages", "UserId");
            CreateIndex("dbo.BankAccounts", "UserId");
            AddForeignKey("dbo.UsersImages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BankAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
