namespace EFCodeFirstPractice.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text.RegularExpressions;
    public class Password : ValidationAttribute
    {
        public Password(
            int minLength, 
            int maxLength, 
            bool containsLowercase, 
            bool containsUppercase,
            bool containsDigits,
            bool containsSpecialSymbol)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
            this.ContainsLowerCase = containsLowercase;
            this.ContainsUpperCase = containsUppercase;
            this.ContainsDigits = containsDigits;
            this.ContainsSpecialSymbol = containsSpecialSymbol;
        }
        public Password(
            int minLength,
            int maxLength,
            bool containsLowercase,
            bool containsUppercase,
            bool containsDigits) 
            : this(
                  minLength,
                  maxLength, 
                  containsLowercase, 
                  containsUppercase, 
                  containsDigits, 
                  false)
        {
        }

        public Password(
            int minLength,
            int maxLength,
            bool containsLowercase,
            bool containsUppercase)
            : this(
                  minLength,
                  maxLength,
                  containsLowercase,
                  containsUppercase,
                  false,
                  false)
        {
        }

        public Password(
            int minLength,
            int maxLength,
            bool containsLowercase)
            : this(
                  minLength,
                  maxLength,
                  containsLowercase,
                  false,
                  false,
                  false)
        {
        }

        public Password(int minLength, int maxLength)
            : this(minLength, maxLength, false, false, false, false)
        {
        }

        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool ContainsLowerCase { get; set; }
        public bool ContainsUpperCase { get; set; }
        public bool ContainsDigits { get; set; }
        public bool ContainsSpecialSymbol { get; set; }
     
        public override bool IsValid(object value)
        {
            string password = Convert.ToString(value);
            string pattern = @"\W_";
            Regex rgx = new Regex(pattern);

            if (password.Length >= this.MinLength 
                && password.Length <= MaxLength
                && this.ContainsDigits
                && this.ContainsLowerCase
                && this.ContainsUpperCase
                && this.ContainsSpecialSymbol
                && password.ToCharArray().Any(ch=>char.IsDigit(ch))
                && password.ToCharArray().Any(ch=>char.IsLower(ch))
                && password.ToCharArray().Any(ch=>char.IsUpper(ch))
                && rgx.IsMatch(password))
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigits
                && this.ContainsLowerCase
                && this.ContainsUpperCase
                && password.ToCharArray().Any(ch => char.IsDigit(ch))
                && password.ToCharArray().Any(ch => char.IsLower(ch))
                && password.ToCharArray().Any(ch => char.IsUpper(ch))
                )
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigits
                && this.ContainsLowerCase
                && password.ToCharArray().Any(ch => char.IsDigit(ch))
                && password.ToCharArray().Any(ch => char.IsLower(ch)))
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigits
                && password.ToCharArray().Any(ch => char.IsDigit(ch)))
            {
                return true;
            }
            else if (password.Length < this.MinLength && password.Length > MaxLength)
            {
                return true;
            }

            return false;
        }
    }
}
