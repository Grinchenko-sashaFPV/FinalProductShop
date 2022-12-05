namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOneToOneMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BankAccounts", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersImages", "User_Id", "dbo.Users");
            DropIndex("dbo.BankAccounts", new[] { "User_Id" });
            DropIndex("dbo.UsersImages", new[] { "User_Id" });
            RenameColumn(table: "dbo.BankAccounts", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.UsersImages", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.BankAccounts", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UsersImages", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.BankAccounts", "UserId");
            CreateIndex("dbo.UsersImages", "UserId");
            AddForeignKey("dbo.BankAccounts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersImages", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersImages", "UserId", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "UserId", "dbo.Users");
            DropIndex("dbo.UsersImages", new[] { "UserId" });
            DropIndex("dbo.BankAccounts", new[] { "UserId" });
            AlterColumn("dbo.UsersImages", "UserId", c => c.Int());
            AlterColumn("dbo.BankAccounts", "UserId", c => c.Int());
            RenameColumn(table: "dbo.UsersImages", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.BankAccounts", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.UsersImages", "User_Id");
            CreateIndex("dbo.BankAccounts", "User_Id");
            AddForeignKey("dbo.UsersImages", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.BankAccounts", "User_Id", "dbo.Users", "Id");
        }
    }
}
