namespace BankSystem.Data.Contracts
{
    using BankSystem.Models;
    public interface ISavingAccount
    {
        string Add(double interest, Account account);
    }
}
