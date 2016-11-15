namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BankSystem.Data.BankSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "BankSystem.Data.BankSystemContext";
        }

        protected override void Seed(BankSystem.Data.BankSystemContext context)
        {
        }
    }
}
