namespace MassDefectSystem.Models
{
    using System.Collections.Generic;

    public class SolarSystem
    {
        private ICollection<Star> starts;
        private ICollection<Planet> planets;

        public SolarSystem()
        {
            this.starts = new HashSet<Star>();
            this.planets = new HashSet<Planet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Star> Stars
        {
            get { return this.starts; }
            set { this.starts = value; }
        }

        public virtual ICollection<Planet> Planets
        {
            get { return this.planets; }
            set { this.planets = value; }
        }
    }
}
