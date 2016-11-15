namespace BankSystem.Data.Contracts
{
    using BankSystem.Models;
    public interface ICheckingAccount
    {
        string Deduct(decimal fee, Account account);
    }
}
