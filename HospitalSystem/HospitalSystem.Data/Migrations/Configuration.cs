namespace HospitalSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using HospitalSystem.Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HospitalSystem.Data.HospitalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HospitalContext";
        }

        protected override void Seed(HospitalSystem.Data.HospitalContext context)
        {
            if (!context.Patients.Any())
            {
                Patient patient = new Patient()
                {
                    FirstName = "Pavel",
                    LastName = "Georgiev",
                    Email = "p.georgiev@gmail.com",
                    DateOfBirth = DateTime.Parse("11/08/1975"),
                    HasInsurance = true
                };

                context.Patients.Add(patient);
                context.SaveChanges();
            }

            if (!context.Medicaments.Any())
            {
                Medicament medicament = new Medicament()
                {
                    Name = "MagicHealer"
                };
                
                context.Medicaments.Add(medicament);
                context.SaveChanges();
            }

            if (!context.Diagnoses.Any())
            {
                Diagnose diagnose = new Diagnose()
                {
                    Name = "Swine flu",
                    Medicament = context.Medicaments.FirstOrDefault()
                };

                context.Diagnoses.Add(diagnose);
                context.Medicaments.First().Diagnose = diagnose;
                context.SaveChanges();
            }

            if (!context.Visitations.Any())
            {
                Visitation visitation = new Visitation()
                {
                    Date = DateTime.Now,
                    Diagnose = context.Diagnoses.FirstOrDefault(),
                    Patient = context.Patients.FirstOrDefault(),
                };

                context.Visitations.Add(visitation);
                context.SaveChanges();
            }
            
            Patient firstPatient = context.Patients.FirstOrDefault();
            Visitation firstPatientVisitation = context.Visitations.FirstOrDefault();
            Diagnose firstPatientDiagnose = context.Diagnoses.FirstOrDefault();

            try
            {
                firstPatient.Visitations.Add(firstPatientVisitation);
                firstPatient.Diagnoses.Add(firstPatientDiagnose);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
