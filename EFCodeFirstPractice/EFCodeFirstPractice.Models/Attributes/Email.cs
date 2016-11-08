namespace EFCodeFirstPractice.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property)]
    public class Email : ValidationAttribute
    {
        #region Use bool IsValid(object value)
        public override bool IsValid(object value)
        {
            string email = Convert.ToString(value);
            string pattern = @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}";
            var rgx = new Regex(pattern);

            if (rgx.IsMatch(email))
            {
                return true;
            }
            return false;
        }
        #endregion


        #region Use ValidationResult IsValid(object value, ValidationContext validationContext)
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var email = Convert.ToString(value);
        //    string pattern = @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}";
        //    var rgx = new Regex(pattern);

        //    try
        //    {
        //        if (rgx.IsMatch(email))
        //        {
        //            return ValidationResult.Success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }

        //    return new ValidationResult("Invalid email address");
        //}
        #endregion

        #region Use CustomAttribute Class
        //public static ValidationResult ValidateEmail(string email)
        //{
        //    string pattern = @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}";
        //    var rgx = new Regex(pattern);

        //    bool isValid = rgx.IsMatch(email);

        //    if (isValid)
        //    {
        //        return ValidationResult.Success;
        //    }

        //    return new ValidationResult("Invalid email");
        //}
        #endregion
    }
}
