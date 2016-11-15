namespace BankSystem.Data
{
    using System.Data.Entity;
    using BankSystem.Models;

    public class BankSystemContext : DbContext
    {
        public BankSystemContext()
            : base("name=BankSystemContext")
        {
        }

        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Account>()
            //    .Property(prop => prop.AccountNumber)
            //    .HasColumnAnnotation(IndexAnnotation.AnnotationName,
            //        new IndexAnnotation(new IndexAttribute("IX_AccountNumber", 1) { IsUnique = true }));

            modelBuilder.Entity<SavingAccount>().ToTable("SavingAccounts");
            modelBuilder.Entity<CheckingAccount>().ToTable("CheckingAccounts");

            base.OnModelCreating(modelBuilder);
        }
    }
}