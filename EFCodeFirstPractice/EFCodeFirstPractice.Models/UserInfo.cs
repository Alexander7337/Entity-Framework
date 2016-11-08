using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstPractice.Models
{
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
