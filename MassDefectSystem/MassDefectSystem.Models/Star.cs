namespace MassDefectSystem.Models
{
    using System.Collections.Generic;

    public class Star
    {
        private ICollection<Planet> planets;

        public Star()
        {
            this.planets = new HashSet<Planet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
        //[ForeignKey("SolarSystem")]
        //public int SolarSystemId { get; set; }
        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Planet> Planets
        {
            get { return this.planets; }
            set { this.planets = value; }
        }
    }
}
