namespace EFCodeFirstPractice.Client
{
    using System;
    using System.Linq;
    using EFCodeFirstPractice.Data;
    using System.Data.Entity.Validation;
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            var context = new CodeFirstPracticeContext();

            var userFullName = context.Users.FirstOrDefault();
            userFullName.Password = "blob100!_N";
            //userFullName.UserInfo.FirstName = "Alexander";
            //userFullName.UserInfo.LastName = "Yanev";
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}",
                                    validationError.PropertyName,
                                    validationError.ErrorMessage);
                    }
                }
            }

            //Console.WriteLine(userFullName.UserInfo.FullName);
            Console.WriteLine(userFullName.Password);
        }
    }
}
