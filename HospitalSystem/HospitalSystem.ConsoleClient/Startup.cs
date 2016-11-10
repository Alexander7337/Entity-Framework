namespace HospitalSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using HospitalSystem.Data;
    public class Startup
    {
        public static void Main()
        {
            var context = new HospitalContext();
            Console.WriteLine(context.Patients.Count());
        }
    }
}
