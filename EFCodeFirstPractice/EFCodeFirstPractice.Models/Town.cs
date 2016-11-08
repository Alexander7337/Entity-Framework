namespace EFCodeFirstPractice.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Town
    {
        [Key]
        public int TownId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
