namespace ClientWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserImageMigration : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Users", "RegistrationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersImages", "UserId", "dbo.Users");
            DropIndex("dbo.UsersImages", new[] { "UserId" });
            DropColumn("dbo.Users", "RegistrationDate");
            DropTable("dbo.UsersImages");
        }
    }
}
