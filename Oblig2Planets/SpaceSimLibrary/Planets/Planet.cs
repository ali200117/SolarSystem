using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceSimLibrary.SpaceObjects;


namespace SpaceSimLibrary.Planets
{
    public class Planet : SpaceObject
    {
        public Planet(string name, string orbits, double distance, double orbitalPeriod, string color, double rotationalPeriod)
            : base(name, SpaceObjectType.Planet, orbits, distance, orbitalPeriod, color, rotationalPeriod) { }
        public List<SpaceObject> Moons { get; set; } = new List<SpaceObject>();


        public class Mercury : Planet
        {
            public Mercury() : base("Mercury", "Sun", 57910, 87.97, "green", 1407.5) { }  
        }

        public class Venus : Planet
        {
            public Venus() : base("Venus", "Sun", 108200, 224.70, "cyan", 5832.0) { }  
        }

        public class Earth : Planet
        {
            public Earth() : base("Earth", "Sun", 149600, 365.26, "blue", 24.0) {
                Moons.Add(new Moons.Moon.TheMoon());
            }  
        }

        public class Mars : Planet
        {
            public Mars() : base("Mars", "Sun", 227940, 686.98, "red", 24.6) {
                Moons.Add(new Moons.Moon.Phobos());
                Moons.Add(new Moons.Moon.Deimos());

            } 
        }

        public class Jupiter : Planet
        {
            public Jupiter() : base("Jupiter", "Sun", 778330, 4332.71, "Orange", 9.9) { } 
        }

        public class Saturn : Planet
        {
            public Saturn() : base("Saturn", "Sun", 1429400, 10759.50, "Yellow", 10.7) { }  
        }

        public class Uranus : Planet
        {
            public Uranus() : base("Uranus", "Sun", 2870990, 30685.00, "cyan", 17.2) { } 
        }

        public class Neptune : Planet
        {
            public Neptune() : base("Neptune", "Sun", 4504300, 60190.00, "lightblue", 16.1) { }  
        }
    }
}
