namespace EFCodeFirstPractice.Data.Migrations
{
    using System;
    using System.Linq;
    using EFCodeFirstPractice.Models;
    using System.Data.Entity.Migrations;
    internal sealed class MyConfiguration : DbMigrationsConfiguration<CodeFirstPracticeContext>
    {
        public MyConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CodeFirstPracticeContext";
        }

        protected override void Seed(CodeFirstPracticeContext context)
        {
            if (!context.WizardDeposits.Any())
            {
                WizardDeposit dumbledore = new WizardDeposit()
                {
                    FirstName = "Albus",
                    LastName = "Dubledore",
                    Age = 150,
                    MagicWandCreator = "Antioch Peverell",
                    MagicWandSize = 15,
                    DepositStartDate = new DateTime(2016, 10, 20),
                    DepositExpirationDate = new DateTime(2020, 10, 20),
                    DepositAmount = 20000.24m,
                    DepositCharge = 0.2,
                    IsDepositExpired = false
                };

                context.WizardDeposits.AddOrUpdate(fn => fn.FirstName, dumbledore);

                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                User user = new User
                {
                    Username = "Alexander7337",
                    Password = "Parola1?",
                    Email = "yanev.alexander@gmail.com"
                };

                context.Users.AddOrUpdate(u => u.Password, user);

                context.SaveChanges();
            }

        }
    }
}
