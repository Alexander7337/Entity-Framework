namespace XmlDataProcessing
{
    using System.Xml.Linq;
    using MassDefectSystem.Models.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.XPath;

    using MassDefectSystem.Models;
    using MassDefectSystem.Data;

    public class XmlDataProcessing
    {
        private const string NewAnomaliesPath = @"../../datasets/new-anomalies.xml";

        static void Main(string[] args)
        {
            var doc = XDocument.Load(NewAnomaliesPath);

            var anomalies = doc.XPathSelectElements("anomalies/anomaly");

            MassDefectContext context = new MassDefectContext();
            foreach (var anomaly in anomalies)
            {
                ImportAnomalyAndVictims(anomaly, context);
            }

            context.SaveChanges();
        }

        private static void ImportAnomalyAndVictims(XElement anomaly, MassDefectContext context)
        {
            var originPlanetName = anomaly.Attribute("origin-planet");
            var teleportPlanetName = anomaly.Attribute("teleport-planet");

            if (originPlanetName == null || teleportPlanetName == null)
            {
                Console.WriteLine("Error: Invalid data.");
            }
            else
            {
                Anomaly newAnomaly = new Anomaly()
                {
                    OriginPlanet = GetPlanetByName(originPlanetName.Value, context),
                    TeleportPlanet = GetPlanetByName(teleportPlanetName.Value, context)
                };

                if (newAnomaly.OriginPlanet == null || newAnomaly.TeleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
                else
                {
                    var victims = anomaly.XPathSelectElements("victims/victim");
                    foreach (var victim in victims)
                    {
                        ImportVictim(victim, context, newAnomaly);
                    }

                    context.SaveChanges();

                    context.Anomalies.Add(newAnomaly);
                    Console.WriteLine("Successfully imported anomaly.");
                }
            }

           

        }

        private static void ImportVictim(XElement victim, MassDefectContext context, Anomaly newAnomaly)
        {
            var name = victim.Attribute("name");

            if (name == null)
            {
                Console.WriteLine("Error: Invalid data.");
            }
            else
            {
                Person person = GetPersonByName(name.Value, context);

                if (person == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
                else
                {
                    newAnomaly.Persons.Add(person);
                }
            }
        }

        private static Person GetPersonByName(string value, MassDefectContext context)
        {
            Person person = context.Persons.FirstOrDefault(e => e.Name == value);

            return person;
        }

        private static Planet GetPlanetByName(string value, MassDefectContext context)
        {
            Planet planet = context.Planets.FirstOrDefault(e => e.Name == value);
            return planet;
        }
    }
}
