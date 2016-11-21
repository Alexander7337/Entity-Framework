namespace ExportDataProcessing
{
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using MassDefectSystem.Data;

    using Newtonsoft.Json;
    public class ExportDataProcessing
    {
        public static void Main(string[] args)
        {
            MassDefectContext context = new MassDefectContext();

            ExportPlanetsWhichAreNotAnomalyOrigins(context);

            ExportPeopleWhichHaveNotBeenVictims(context);

            ExportTopAnomaly(context);

            ExportAnomaliesStatsInXml(context);
        }

        private static void ExportAnomaliesStatsInXml(MassDefectContext context)
        {
            var exportedAnomalies = context.Anomalies
                            .OrderBy(e => e.Id)
                            .Select(anomaly => new
                            {
                                id = anomaly.Id,
                                originPlanetName = anomaly.OriginPlanet.Name,
                                teleportPlanetName = anomaly.TeleportPlanet.Name,
                                victims = anomaly.Persons.Select(e => e.Name)
                            })
                            .ToList();

            var xmlDocument = new XElement("anomalies");

            foreach (var exportedAnomaly in exportedAnomalies)
            {
                var anomalyNode = new XElement("anomaly");
                anomalyNode.Add(new XAttribute("id", exportedAnomaly.id));
                anomalyNode.Add(new XAttribute("origin-planet", exportedAnomaly.originPlanetName));
                anomalyNode.Add(new XAttribute("teleport-planet", exportedAnomaly.teleportPlanetName));

                var victimsNode = new XElement("victims");
                foreach (var victim in exportedAnomaly.victims)
                {
                    var victimNode = new XElement("victim");
                    victimNode.Add(new XAttribute("name", victim));
                    victimsNode.Add(victimNode);
                }

                anomalyNode.Add(victimsNode);
                xmlDocument.Add(anomalyNode);
            }

            xmlDocument.Save(@"../../anomalies.xml");
        }

        private static void ExportTopAnomaly(MassDefectContext context)
        {
            var topAnomaly = context.Anomalies
                .OrderByDescending(e => e.Persons.Count)
                .Take(1)
                .Select(e => new
                {
                    e.Id,
                    originPlanet = new
                    {
                        name = e.OriginPlanet.Name
                    },
                    teleportPlanet = new
                    {
                        name = e.TeleportPlanet.Name
                    },
                    victimsCount = e.Persons.Count
                });

            var anomalyAsJson = JsonConvert.SerializeObject(topAnomaly, Formatting.Indented);
            File.WriteAllText(@"../../anomaly.json", anomalyAsJson);

        }

        private static void ExportPeopleWhichHaveNotBeenVictims(MassDefectContext context)
        {
            var peopleNotVictims = context.Persons
                .Where(e => !e.Anomalies.Any())
                .Select(e => new
                {
                    e.Name,
                    homePlanet = new
                    {
                        e.HomePlanet.Name
                    }
                });

            var peopleAsJson = JsonConvert.SerializeObject(peopleNotVictims, Formatting.Indented);
            File.WriteAllText(@"../../people.json", peopleAsJson);
        }

        private static void ExportPlanetsWhichAreNotAnomalyOrigins(MassDefectContext context)
        {
            var exportedPlanets = context.Planets
                .Where(planet => !planet.OriginAnomalies.Any())
                .Select(planet => new
                {
                    name = planet.Name
                });


            var planetsAsJson = JsonConvert.SerializeObject(exportedPlanets, Formatting.Indented);
            File.WriteAllText(@"../../planets.json", planetsAsJson);
        }
    }
}
