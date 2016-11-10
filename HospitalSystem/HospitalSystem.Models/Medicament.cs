namespace HospitalSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Medicament
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MedicamentId { get; set; }
        [Required]
        public string Name { get; set; }
        //[ForeignKey("Diagnose")]
        //public int? DiagnoseId { get; set; }
        public virtual Diagnose Diagnose { get; set; }
    }
}
