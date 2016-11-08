using System.Linq;
using System.Text.RegularExpressions;

namespace EFCodeFirstPractice.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Pass : ValidationAttribute
    {
        private int minLength;
        private int maxLength;
        public Pass(int minLength, int maxLength)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }
        protected Pass(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }
        protected Pass(string errorMessage) : base(errorMessage)
        {
        }

        public int MinLength
        {
            get { return this.minLength; }
            set { this.minLength = value; }
        }

        public int MaxLength
        {
            get { return this.maxLength; }
            set { this.maxLength = value; }
        }

        public bool ContainsLowercase { get; set; }
        public bool ContainsUppercase { get; set; }
        public bool ContainsDigit { get; set; }
        public bool ContainsSpecialSymbol { get; set; }

        public override bool IsValid(object value)
        {
            string password = Convert.ToString(value);
            string pattern = @"\W_";
            Regex rgx = new Regex(pattern);

            if (password.Length >= this.MinLength
                && password.Length <= MaxLength
                && this.ContainsDigit
                && this.ContainsLowercase
                && this.ContainsUppercase
                && this.ContainsSpecialSymbol
                && password.ToCharArray().Any(ch => char.IsDigit(ch))
                && password.ToCharArray().Any(ch => char.IsLower(ch))
                && password.ToCharArray().Any(ch => char.IsUpper(ch))
                && rgx.IsMatch(password))
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigit
                && this.ContainsLowercase
                && this.ContainsUppercase
                && password.ToCharArray().Any(ch => char.IsDigit(ch))
                && password.ToCharArray().Any(ch => char.IsLower(ch))
                && password.ToCharArray().Any(ch => char.IsUpper(ch))
                )
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigit
                && this.ContainsLowercase
                && password.ToCharArray().Any(ch => char.IsDigit(ch))
                && password.ToCharArray().Any(ch => char.IsLower(ch)))
            {
                return true;
            }
            else if (password.Length < this.MinLength
                && password.Length > MaxLength
                && this.ContainsDigit
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
