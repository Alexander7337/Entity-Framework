namespace EFCodeFirstPractice.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class WizardDeposit
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(60)]
        public string LastName { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        [MaxLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, Int16.MaxValue)]
        public int? MagicWandSize { get; set; }

        [MaxLength(20)]
        public string DepositGroup { get; set; }

        public DateTime? DepositStartDate { get; set; }

        public decimal? DepositAmount { get; set; }

        public double? DepositInterest  { get; set; }

        public double? DepositCharge { get; set; }

        public DateTime? DepositExpirationDate { get; set; }

        public bool? IsDepositExpired { get; set; }
    }
}
