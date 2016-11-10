namespace HospitalSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Patient
    {
        private ICollection<Visitation> visitations;
        private ICollection<Diagnose> diagnoses;

        public Patient()
        {
            this.visitations = new HashSet<Visitation>();
            this.diagnoses = new HashSet<Diagnose>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public bool HasInsurance { get; set; }
        public virtual ICollection<Visitation> Visitations { get; set; }
        public virtual ICollection<Diagnose> Diagnoses { get; set; }
    }
}
