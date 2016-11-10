namespace HospitalSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Diagnose
    {
        private ICollection<Patient> patients;

        public Diagnose()
        {
            this.patients = new HashSet<Patient>();
        }

        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiagnoseId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Comment { get; set; }
        //[ForeignKey("Visitation")]
        //public int? VisitationId { get; set; }
        public virtual Visitation Visitation { get; set; }
        //[ForeignKey("Medicament")]
       // public int? MedicamentId { get; set; }
        public virtual Medicament Medicament { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
