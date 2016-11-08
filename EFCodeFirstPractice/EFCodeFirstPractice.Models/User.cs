namespace EFCodeFirstPractice.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EFCodeFirstPractice.Models.Attributes;
    using System.ComponentModel.DataAnnotations.Schema;
    public class User
    {
        [Key]
        public int Id { get; set; }

        public UserInfo UserInfo { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]

        //[Password(4, 20, true, true, true, true)]

        [Pass(4, 20, ContainsLowercase = true, ContainsUppercase = true, ContainsDigit = true, ContainsSpecialSymbol = true)]
        
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[\W_])[A-Za-z\d\W_]{4,}$", 
        //    ErrorMessage="Password should have at least 1 small letter, 1 capital letter, 1 digit and 1 special character")]
        public string Password { get; set; }

        [Required]

        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$", 
        //    ErrorMessage = "Invalid email address")]
        //[CustomValidation(typeof(Email), "ValidateEmail")]

        [Email]
        public string Email { get; set; }

        [MaxLength(1048576)]
        public string ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1, 120)]
        public byte? Age { get; set; }
        
        public bool? IsDeleted { get; set; }

        [ForeignKey("Town")]
        public int? TownId { get; set; }

        public Town Town { get; set; }
    }
}
