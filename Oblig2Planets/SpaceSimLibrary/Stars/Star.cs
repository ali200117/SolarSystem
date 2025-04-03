using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpaceSimLibrary.SpaceObjects;

namespace SpaceSimLibrary.Stars
{
    public class Star : SpaceObject
    {
        public Star(string name, string orbits, double distance, double orbitalPeriod, string color, double rotationalPeriod)
            : base(name, SpaceObjectType.Star, orbits, distance, orbitalPeriod, color, rotationalPeriod) { }

        public class Sun : Star
        {
            public Sun() : base("Sun", "-", 0, 0, "Yellow", 25.0) { } 
        }
    }
}
