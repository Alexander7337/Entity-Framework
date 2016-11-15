namespace BankSystem.ConsoleClient.IO
{
    using System;
    public class InputReader
    {
        private const string EndCommand = "quit";

        private CommandInterpreter interpreter;

        public InputReader(CommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public CommandInterpreter Interpreter
        {
            get { return this.interpreter; }
            set { this.interpreter = value; }
        }
        public void StartReadingCommands()
        {
            Console.WriteLine("First log into the system. Then, state your command/s:");
            
            string input = this.ReadLine();

            while (input != EndCommand)
            {
                Console.WriteLine("Command is being processed...");

                Interpreter.InterpretCommand(input);

                input = this.ReadLine();
            }
        }

        private string ReadLine()
        {
            string reader = Console.ReadLine();
            return reader;
        }
    }
}
