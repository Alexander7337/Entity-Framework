namespace EFCodeFirstPractice.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using EFCodeFirstPractice.Models;
    using EFCodeFirstPractice.Data.Migrations;

    public class CodeFirstPracticeContext : DbContext
    {
        public CodeFirstPracticeContext()
            : base("CodeFirstPracticeContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<CodeFirstPracticeContext, MyConfiguration>();
            Database.SetInitializer(migrationStrategy);
        }

        public IDbSet<WizardDeposit> WizardDeposits { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Country> Countries { get; set; }
    }
}