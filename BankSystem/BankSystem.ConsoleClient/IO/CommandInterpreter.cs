namespace BankSystem.ConsoleClient.IO
{
    using System;
    using System.Linq;
    using System.Text;
    
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.Data.Repositories;

    public class CommandInterpreter
    {
        private readonly string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int LengthOfAccountNumber = 10;

        private BankSystemContext context;
        private OperationsManager operationsManager;
        private User user;

        public CommandInterpreter(
            BankSystemContext context, 
            OperationsManager operationsManager,
            User user)
        {
            this.context = context;
            this.operationsManager = operationsManager;
            this.user = user;
        }

        public BankSystemContext Context
        {
            get { return this.context; }
            set { this.context = value; }
        }

        public OperationsManager OperationsManager
        {
            get { return this.operationsManager; }
            set { this.operationsManager = value; }
        }

        public User User
        {
            get { return this.user; }
            set { this.user = value; }
        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split(' ');
            string command = data[0];
            command = command.ToLower();

            try
            {
                this.ParseCommand(input, data, command);
            }
            catch (Exception ex)
            {
                OutputWriter.WriteMessage(ex.Message);
            }
        }

        public void ParseCommand(string input, string[] data, string command)
        {
            switch (command)
            {
                case "register":
                    TryRegisterNewUser(input, data);
                    break;
                case "login":
                    TryLogin(input, data);
                    break;
                case "logout":
                    TryLogout();
                    break;
                case "listaccounts":
                    TryListAccounts();
                    break;
                case "add":
                    bool isTrue = data[1] == "savingaccount";
                    if (isTrue)
                    {
                        TryAddSavingAccount(input, data);
                    }
                    else
                    {
                        TryAddCheckingAccount(input, data);
                    }
                    break;
                case "deposit":
                    TryDepositCommand(input, data);
                    break;
                case "withdraw":
                    TryWithdrawCommand(input, data);
                    break;
                case "deductfee":
                    TryDeductFee(input, data);
                    break;
                case "addinterest":
                    TryAddInterest(input, data);
                    break;
                default:
                    OutputWriter.WriteMessage("Invalid command");
                    break;
            }
        }

        private void TryAddInterest(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                string accountNumber = data[1];
                SavingAccount account = (SavingAccount)context.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);
                double interest = account.InterestRate;

                string result = this.OperationsManager.Add(interest, account);

                OutputWriter.WriteMessage(result);
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryDeductFee(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                string accountNumber = data[1];

                CheckingAccount account = (CheckingAccount)context.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);
                decimal fee = account.Fee;

                string result = this.OperationsManager.Deduct(fee, account);

                OutputWriter.WriteMessage(result);
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryDepositCommand(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                string accountNumber = data[1];
                decimal money = decimal.Parse(data[2]);

                var account = context.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);

                string result = this.OperationsManager.Deposit(money, account);

                OutputWriter.WriteMessage(result);
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryWithdrawCommand(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                string accountNumber = data[1];
                var account = context.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);
                decimal money = decimal.Parse(data[2]);

                string result = this.OperationsManager.Withdraw(money, account);

                OutputWriter.WriteMessage(result);
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryListAccounts()
        {
            if (this.IsLoggedIn())
            {
                var savingAccounts = context.Accounts.OfType<SavingAccount>().Where(acc => acc.Holder.UserId == this.User.UserId);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Saving Accounts:");

                foreach (var savingAccount in savingAccounts)
                {
                    sb.AppendLine($"{savingAccount.AccountNumber} {savingAccount.Balance}");
                }

                var checkingAccounts = context.Accounts.OfType<CheckingAccount>().Where(acc => acc.Holder.UserId == this.User.UserId);
                sb.AppendLine($"Checking Accounts:");
                foreach (var checkingAccount in checkingAccounts)
                {
                    sb.AppendLine($"{checkingAccount.AccountNumber} {checkingAccount.Balance}");
                }

                OutputWriter.WriteMessage(sb.ToString());
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryAddCheckingAccount(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                try
                {
                    string accountNumber = GenerateRandomAccountNumber();
                    decimal balance = decimal.Parse(data[2]);
                    decimal fee = decimal.Parse(data[3]);

                    Account newAccount = new CheckingAccount()
                    {
                        AccountNumber = accountNumber,
                        Balance = balance,
                        Fee = fee
                    };

                    context.Accounts.Add(newAccount);
                    this.User.Accounts.Add(newAccount);

                    context.SaveChanges();

                    OutputWriter.WriteMessage("Checking account was successfully added.");
                }
                catch (Exception ex)
                {
                    OutputWriter.WriteMessage(ex.Message);
                }
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private void TryAddSavingAccount(string input, string[] data)
        {
            if (this.IsLoggedIn())
            {
                try
                {
                    string accountNumber = GenerateRandomAccountNumber();
                    decimal balance = decimal.Parse(data[2]);
                    double interest = double.Parse(data[3]);

                    Account newAccount = new SavingAccount()
                    {
                        AccountNumber = accountNumber,
                        Balance = balance,
                        InterestRate = interest
                    };

                    context.Accounts.Add(newAccount);
                    this.User.Accounts.Add(newAccount);

                    context.SaveChanges();

                    OutputWriter.WriteMessage("Account was successfully added.");
                }
                catch (Exception ex)
                {
                    OutputWriter.WriteMessage(ex.Message);
                }
            }
            else
            {
                OutputWriter.WriteMessage("You are not logged-in.");
            }
        }

        private string GenerateRandomAccountNumber()
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            string numberAsString = String.Empty;

            for (int i = 0; i < LengthOfAccountNumber; i++)
            {
                numberAsString += AllowedCharacters[random.Next(0, AllowedCharacters.Length)];
            }

            return numberAsString;
        }

        private void TryLogout()
        {
            user = new User();
            OutputWriter.WriteMessage("Logged out");
        }

        private void TryLogin(string input, string[] data)
        {
            if (data.Length != 3)
            {
                throw new ArgumentException("Input does not match Login requirements");
            }

            string username = data[1];
            string password = data[2];

            User loggedUser = context.Users.FirstOrDefault(ur => ur.Username == username && ur.Password == password);

            if (loggedUser == null)
            {
                OutputWriter.WriteMessage("Returned null user's values");
            }

            this.user = loggedUser;
            OutputWriter.WriteMessage("You are logged in.");
        }

        private bool IsLoggedIn()
        {
            if (this.user.Username == null)
            {
                throw new ArgumentException("Login is required.");
            }

            return true;
        }

        private void TryRegisterNewUser(string input, string[] data)
        {
            if (data.Length != 4)
            {
                OutputWriter.WriteMessage("Command does not match Register requirements");
                return;
            }

            string username = data[1];
            string password = data[2];
            string email = data[3];

            User newUser = new User()
            {
                Email = email,
                Username = username,
                Password = password
            };

            this.Context.Users.Add(newUser);

            context.SaveChanges();

            OutputWriter.WriteMessage("Successfully registered!");
        }
    }
}
