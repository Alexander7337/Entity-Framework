namespace HospitalSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using HospitalSystem.Data.Migrations;
    using HospitalSystem.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class HospitalContext : DbContext
    {
        public HospitalContext()
            : base("name=HospitalContext")
        {
            var migration = new MigrateDatabaseToLatestVersion<HospitalContext, Configuration>();
            Database.SetInitializer(migration);
        }
        public IDbSet<Diagnose> Diagnoses { get; set; }
        public IDbSet<Medicament> Medicaments { get; set; }
        public IDbSet<Patient> Patients { get; set; }
        public IDbSet<Visitation> Visitations { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diagnose>()
                .HasMany<Patient>(d => d.Patients)
                .WithMany()
                .Map(dp =>
                {
                    dp.MapLeftKey("DianoseId");
                    dp.MapRightKey("PatientId");
                    dp.ToTable("DiagnosePatients");
                });

            modelBuilder.Entity<Medicament>()
                .HasKey(k => k.MedicamentId)
                .Property(k => k.MedicamentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Medicament>()
                .HasOptional(m => m.Diagnose)
                .WithOptionalPrincipal(d => d.Medicament);

            modelBuilder.Entity<Visitation>()
                .HasOptional(v => v.Diagnose)
                .WithOptionalPrincipal(d => d.Visitation);


            base.OnModelCreating(modelBuilder);
        }
    }
}