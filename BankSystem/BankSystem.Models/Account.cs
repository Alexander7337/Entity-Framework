namespace BankSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    public abstract class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public virtual User Holder { get; set; }
    }
}
