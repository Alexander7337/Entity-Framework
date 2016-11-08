namespace EFCodeFirstPractice.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using EFCodeFirstPractice.Models;
    using EFCodeFirstPractice.Data.Migrations;

    public class CodeFirstPracticeContext : DbContext
    {
        // Your context has been configured to use a 'CodeFirstPracticeEntities' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EFCodeFirstPractice.Data.CodeFirstPracticeEntities' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'CodeFirstPracticeEntities' 
        // connection string in the application configuration file.
        public CodeFirstPracticeContext()
            : base("CodeFirstPracticeContext")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<CodeFirstPracticeContext, MyConfiguration>();
            Database.SetInitializer(migrationStrategy);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public IDbSet<WizardDeposit> WizardDeposits { get; set; }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Town> Towns { get; set; }

        public IDbSet<Country> Countries { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}