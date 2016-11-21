namespace MassDefectSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using MassDefectSystem.Data.Migrations;
    using MassDefectSystem.Models;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
            var strategy = new MigrateDatabaseToLatestVersion<MassDefectContext, Configuration>();
            Database.SetInitializer(strategy);
        }
        
        public DbSet<Anomaly> Anomalies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<SolarSystem> SolarSystems { get; set; }
        public DbSet<Star> Stars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasMany<Person>(a => a.Persons)
                .WithMany(p => p.Anomalies)
                .Map(e =>
                {
                    e.MapLeftKey("AnomalyId");
                    e.MapRightKey("PersonId");
                    e.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}