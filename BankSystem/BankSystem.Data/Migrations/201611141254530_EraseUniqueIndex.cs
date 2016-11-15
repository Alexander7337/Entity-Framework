namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EraseUniqueIndex : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Accounts", new[] { "AccountNumber" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Accounts", "AccountNumber", unique: true);
        }
    }
}
