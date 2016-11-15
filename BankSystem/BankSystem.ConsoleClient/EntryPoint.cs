namespace BankSystem.ConsoleClient
{
    using BankSystem.Models;
    using BankSystem.Data;
    using BankSystem.ConsoleClient.IO;
    using BankSystem.Data.Repositories;
    public class EntryPoint
    {
        public static void Main()
        {
            BankSystemContext context = new BankSystemContext();
            OperationsManager manager = new OperationsManager(context);
            
            User userLoginState = new User();
            CommandInterpreter interpreter = new CommandInterpreter(context, manager, userLoginState);
            InputReader reader = new InputReader(interpreter);

            reader.StartReadingCommands();
        }
    }
}
