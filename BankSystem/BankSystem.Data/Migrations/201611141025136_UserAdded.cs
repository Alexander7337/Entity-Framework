namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            AddColumn("dbo.Accounts", "Holder_UserId", c => c.Int());
            CreateIndex("dbo.Accounts", "Holder_UserId");
            AddForeignKey("dbo.Accounts", "Holder_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "Holder_UserId", "dbo.Users");
            DropIndex("dbo.Accounts", new[] { "Holder_UserId" });
            DropColumn("dbo.Accounts", "Holder_UserId");
            DropTable("dbo.Users");
        }
    }
}
