using System;
using System.Collections.Generic;
using SpaceSimLibrary.Moons;
using SpaceSimLibrary.Planets;
using SpaceSimLibrary.Stars;
using SpaceSimLibrary.SpaceObjects;
using SpaceSimLibrary;
using static SpaceSimLibrary.Moons.Moon;
using static SpaceSimLibrary.Planets.Planet;

namespace SpaceSim
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Be om input for tid (t) fra brukeren
            Console.WriteLine("Enter time (in days): ");
            double time = Convert.ToDouble(Console.ReadLine());

            // Be om planetenavn fra brukeren
            Console.WriteLine("Enter the name of the planet (or press Enter for Sun): ");
            string planetName = Console.ReadLine();

            // Lager en liste med objektene i solsystemet
            List<SpaceObject> solarSystem = new List<SpaceObject>
            {
                new Mercury(),
                new Venus(),
                new Earth(),
                new Mars(),
                new Jupiter(),
                new Saturn(),
                new Uranus(),
                new Neptune(),
                new Pluto(),
                new TheMoon(),
                new Phobos(),
                new Deimos()
            };

            // Hvis brukeren ikke skriver inn et planetnavn, bruker vi "Sun" som standard
            if (string.IsNullOrEmpty(planetName))
            {
                planetName = "Sun";
            }

            // Sjekk om planeten finnes i solsystemet
            SpaceObject selectedPlanet = solarSystem.Find(obj => obj.Name.Equals(planetName, StringComparison.OrdinalIgnoreCase));

            if (selectedPlanet != null)
            {
                // Beregn posisjonen til den valgte planeten
                selectedPlanet.CalculatePosition(time);

                // Hvis planeten har måner, beregn deres posisjoner
                foreach (var obj in solarSystem)
                {
                    if (obj.Orbits.Equals(planetName, StringComparison.OrdinalIgnoreCase)) // Sjekk om månen tilhører den valgte planeten
                    {
                        obj.CalculatePosition(time);  // Beregn månenes posisjon
                    }
                }
            }
            else
            {
                Console.WriteLine("Planet not found. Showing details for the Sun and all planets:");
                // Hvis planeten ikke finnes, skriv ut detaljer for solen og alle planetene
                foreach (var obj in solarSystem)
                {
                    obj.CalculatePosition(time);
                }
            }

            Console.ReadLine(); // Holder konsollvinduet åpent
        }
    }
}
