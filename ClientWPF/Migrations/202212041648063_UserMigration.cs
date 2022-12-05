namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MoneyAmount = c.Double(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UsersImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.Binary(),
                        FileExtension = c.String(),
                        Size = c.Decimal(nullable: false, precision: 18, scale: 2),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersImages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "User_Id", "dbo.Users");
            DropIndex("dbo.UsersImages", new[] { "User_Id" });
            DropIndex("dbo.BankAccounts", new[] { "User_Id" });
            DropTable("dbo.UsersImages");
            DropTable("dbo.BankAccounts");
        }
    }
}
