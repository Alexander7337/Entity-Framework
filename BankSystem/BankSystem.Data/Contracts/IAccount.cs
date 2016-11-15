namespace BankSystem.Data.Contracts
{
    using BankSystem.Models;
    public interface IAccount : ISavingAccount, ICheckingAccount
    {
        string Deposit(decimal money, Account account);
        string Withdraw(decimal money, Account account);
    }
}
