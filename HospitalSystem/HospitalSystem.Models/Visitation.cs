namespace HospitalSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Visitation
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VisitationId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        //[ForeignKey("Diagnose")]
        //public int? DiagnoseId { get; set; }
        public virtual Diagnose Diagnose { get; set; }
        //[ForeignKey("Patient")]
        //public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        //[ForeignKey("Medicament")]
        //public int? MedicamentId { get; set; }
        //public virtual Medicament Medicament { get; set; }
    }
}
