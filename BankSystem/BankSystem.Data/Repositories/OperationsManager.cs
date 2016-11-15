namespace BankSystem.Data.Repositories
{
    using System;
    using BankSystem.Models;
    using System.Data.Entity;
    using BankSystem.Data.Contracts;

    public class OperationsManager : IAccount
    {
        private DbContext context;

        public OperationsManager(DbContext context)
        {
            if (context != null)
            {
                this.context = context;
            }
        }

        public DbContext Context
        {
            get { return this.context; }
            set { this.context = value; }
        }

        public string Deposit(decimal money, Account account)
        {
            using (var customTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    account.Balance += money;
                    context.SaveChanges();
                    customTransaction.Commit();
                    return "Money has been deposited.";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Withdraw(decimal money, Account account)
        {
            using (var customTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (account.Balance >= money)
                    {
                        account.Balance -= money;
                        context.SaveChanges();
                        customTransaction.Commit();
                        return "Money has been withdrawed.";
                    }
                    else
                    {
                        customTransaction.Rollback();
                        return "Not enough money";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Deduct(decimal fee, Account account)
        {
            using (var customTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (account.Balance >= fee)
                    {
                        account.Balance -= fee;
                        context.SaveChanges();
                        customTransaction.Commit();
                        return "Fee was deducted successfully.";
                    }
                    else
                    {
                        customTransaction.Rollback();
                        return "Not enough money";
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public string Add(double interest, Account account)
        {
            using (var customTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    decimal calc = Convert.ToDecimal((interest + 100) / 100);
                    decimal newBalance = Decimal.Multiply(account.Balance, calc);
                    account.Balance = newBalance;
                    context.SaveChanges();
                    customTransaction.Commit();
                    return "Interest successfully added.";
                }
                catch (Exception ex)
                {
                    customTransaction.Rollback();
                    return ex.Message;
                }
            }
        }
    }
}
