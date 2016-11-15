namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AccountId)
                .Index(t => t.AccountNumber, unique: true);
            
            CreateTable(
                "dbo.SavingAccounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        InterestRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.CheckingAccounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckingAccounts", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.SavingAccounts", "AccountId", "dbo.Accounts");
            DropIndex("dbo.CheckingAccounts", new[] { "AccountId" });
            DropIndex("dbo.SavingAccounts", new[] { "AccountId" });
            DropIndex("dbo.Accounts", new[] { "AccountNumber" });
            DropTable("dbo.CheckingAccounts");
            DropTable("dbo.SavingAccounts");
            DropTable("dbo.Accounts");
        }
    }
}
