namespace MassDefectSystem.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Data.Entity.Validation;

    using MassDefectSystem.Data;
    using MassDefectSystem.Models;
    using MassDefectSystem.Models.DTO;

    using Newtonsoft.Json;
    public class Startup
    {
        private const string SolarSystemPath = @"../../datasets/solar-systems.json";
        private const string StarsPath = @"../../datasets/stars.json";
        private const string PlanetsPath = @"../../datasets/planets.json";
        private const string PersonsPath = @"../../datasets/persons.json";
        private const string AnomaliesPath = @"../../datasets/anomalies.json";
        private const string AnomalyVictimsPath = @"../../datasets/anomaly-victims.json";

        public static void Main()
        {
            MassDefectContext context = new MassDefectContext();
            context.Database.Initialize(true);

            ImportSolarSystems();

            ImportStars();

            ImportPlanets();

            ImportPersons();

            ImportAnomalies();

            ImportAnomalyVictims();
        }

        private static void ImportAnomalyVictims()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(AnomalyVictimsPath);
            var anomalyVictims = JsonConvert.DeserializeObject<IEnumerable<AnomalyVictimsDTO>>(json);

            foreach (var anomalyVictim in anomalyVictims)
            {
                if (anomalyVictim.Id == null || anomalyVictim.Person == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                Anomaly anomaly = GetAnomalyById(anomalyVictim.Id, context);
                Person person = GetPersonByName(anomalyVictim.Person, context);

                if (anomaly == null || person == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                anomaly.Persons.Add(person);
            }

            context.SaveChanges();
        }

        private static Person GetPersonByName(string name, MassDefectContext context)
        {
            Person person = context.Persons.FirstOrDefault(e => e.Name == name);
            return person;
        }

        private static Anomaly GetAnomalyById(int? input, MassDefectContext context)
        {
            Anomaly anomaly = context.Anomalies.Find(input);

            return anomaly;
        }

        private static void ImportAnomalies()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(AnomaliesPath);
            var anomalies = JsonConvert.DeserializeObject<IEnumerable<AnomalyDTO>>(json);

            foreach (var anomaly in anomalies)
            {
                if (anomaly.OriginPlanet == null || anomaly.TeleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                Anomaly newAnomaly = new Anomaly()
                {
                    OriginPlanet = GetPlanetByName(anomaly.OriginPlanet, context),
                    TeleportPlanet = GetPlanetByName(anomaly.TeleportPlanet, context)
                };

                if (newAnomaly.OriginPlanet == null || newAnomaly.TeleportPlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                context.Anomalies.Add(newAnomaly);
                Console.WriteLine("Successfully imported anomaly.");
            }

            context.SaveChanges();
        }

        private static void ImportPersons()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(PersonsPath);
            var persons = JsonConvert.DeserializeObject<IEnumerable<PersonDTO>>(json);

            foreach (var person in persons)
            {
                if (person.Name == null || person.HomePlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                Person newPerson = new Person()
                {
                    Name = person.Name,
                    HomePlanet = GetPlanetByName(person.HomePlanet, context)
                };

                if (newPerson.HomePlanet == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                context.Persons.Add(newPerson);

                Console.WriteLine($"Successfully imported {newPerson.GetType().Name} {newPerson.Name}.");
            }

            context.SaveChanges();
        }

        private static Planet GetPlanetByName(string name, MassDefectContext context)
        {
            Planet planet = context.Planets.FirstOrDefault(e => e.Name == name);
            return planet;
        }

        private static void ImportPlanets()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(PlanetsPath);
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);

            int nextId = 1;
            Planet lastPlanetId = context.Planets.OrderByDescending(e => e.Id).FirstOrDefault();
            if (lastPlanetId != null)
            {
                nextId = context.Planets.OrderByDescending(e => e.Id).FirstOrDefault().Id;
            }

            foreach (var planet in planets)
            {
                if (planet.Name == null || planet.Sun == null || planet.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                Planet newPlanet = new Planet()
                {
                    Name = planet.Name,
                    Sun = GetSunByName(planet.Sun, context),
                    SolarSystem = GetSolarSystem(planet.SolarSystem, context)
                };

                if (newPlanet.Sun == null || newPlanet.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    continue;
                }

                newPlanet.Id = nextId;
                nextId++;

                context.Planets.Add(newPlanet);
                
                Console.WriteLine($"Successfully imported {newPlanet.GetType().Name} {newPlanet.Name}.");
            }
            
            context.SaveChanges();
        }

        private static Star GetSunByName(string name, MassDefectContext context)
        {
            Star star = context.Stars.FirstOrDefault(e => e.Name == name);
            return star;
        }

        private static void ImportStars()
        {
            MassDefectContext context = new MassDefectContext();

            var json = File.ReadAllText(StarsPath);
            var stars = JsonConvert.DeserializeObject<IEnumerable<StarDTO>>(json);

            foreach (var star in stars)
            {
                if (star.Name == null || star.SolarSystem == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                }
                else
                {
                    Star newStar = new Star()
                    {
                        Name = star.Name,
                        SolarSystem = GetSolarSystem(star.SolarSystem, context)
                    };

                    if (newStar.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid data.");
                        continue;
                    }

                    context.Stars.Add(newStar);

                    Console.WriteLine($"Successfully imported {newStar.GetType().Name} {newStar.Name}.");
                }
            }

            context.SaveChanges();
        }

        private static SolarSystem GetSolarSystem(string name, MassDefectContext context)
        {
            SolarSystem solarSystem = context.SolarSystems.FirstOrDefault(e => e.Name == name);

            return solarSystem;
        }

        private static void ImportSolarSystems()
        {
            MassDefectContext context = new MassDefectContext();
            var json = File.ReadAllText(SolarSystemPath);
            var solarSystems = JsonConvert.DeserializeObject<IEnumerable<SolarSystemDTO>>(json);

            foreach (var solarSystem in solarSystems)
            {
                if (solarSystem.Name == null)
                {
                    Console.WriteLine("Error: Invalid data.");
                    //continue;
                }
                else
                {
                    SolarSystem newSolarSystem = new SolarSystem()
                    {
                        Name = solarSystem.Name
                    };

                    context.SolarSystems.Add(newSolarSystem);
                    Console.WriteLine($"Successfully imported {newSolarSystem.GetType().Name} {newSolarSystem.Name}.");
                }
            }

            context.SaveChanges();
        }
    }
}
