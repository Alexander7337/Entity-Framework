namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfUniqueIndexFromIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accounts", "AccountNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "AccountNumber", c => c.Int(nullable: false));
        }
    }
}
