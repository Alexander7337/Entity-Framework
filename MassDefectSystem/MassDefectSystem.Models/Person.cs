namespace MassDefectSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Person
    {
        private ICollection<Anomaly> anomalies;

        public Person()
        {
            this.anomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("HomePlanet")]
        public int PlanetId { get; set; }
        public virtual Planet HomePlanet { get; set; }

        public virtual ICollection<Anomaly> Anomalies
        {
            get { return this.anomalies; }
            set { this.anomalies = value; }
        }
    }
}
