namespace EFCodeFirstPractice.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [ComplexType]
    public class UserInfo
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return this.FirstName + ' ' + this.LastName; }
        }
    }
}
