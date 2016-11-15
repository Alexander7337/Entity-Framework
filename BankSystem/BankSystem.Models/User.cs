namespace BankSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        private ICollection<Account> accounts;

        public User()
        {
            this.accounts = new HashSet<Account>();
        }

        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
