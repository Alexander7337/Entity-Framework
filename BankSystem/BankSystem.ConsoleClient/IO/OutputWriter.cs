namespace BankSystem.ConsoleClient.IO
{
    using System;
    public static class OutputWriter
    {
        public static void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void WriteEmptyLine()
        {
            Console.WriteLine();
        }
    }
}
