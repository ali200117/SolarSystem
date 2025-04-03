using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceSimLibrary.Planets;
using SpaceSimLibrary.SpaceObjects;

namespace SpaceSimLibrary.Moons
{
    public class Moon : SpaceObject
    {
        public Moon(string name, string orbits, double distance, double orbitalPeriod, string color, double rotationalPeriod)
            : base(name, SpaceObjectType.Moon, orbits, distance, orbitalPeriod, color, rotationalPeriod) { }

        public class TheMoon : Moon
        {
            public TheMoon() : base("The Moon", "Earth", 384, 27.32, "Grey", 708.7) { }  
        }

        public class Phobos : Moon
        {
            public Phobos() : base("Phobos", "Mars", 9, 0.32, "white", 7.7) { } 
        }

        public class Deimos : Moon
        {
            public Deimos() : base("Deimos", "Mars", 23, 1.26, "Orange", 30.3) { }  
        }
    }
}
