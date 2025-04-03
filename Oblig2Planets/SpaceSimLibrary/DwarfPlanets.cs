using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceSimLibrary.SpaceObjects;

namespace SpaceSimLibrary
{
    public class DwarfPlanet : SpaceObject
    {
        public DwarfPlanet(string name, string orbits, double distance, double orbitalPeriod, string color, double rotationalPeriod)
             : base(name, SpaceObjectType.DwarfPlanet, orbits, distance, orbitalPeriod, color, rotationalPeriod) { }
    }

    // Eksempler på dvergplaneter med rotasjonstid
    public class Pluto : DwarfPlanet
    {
        public Pluto() : base("Pluto", "Sun", 5913520, 90550.00, "dark yellow", 153.3) { } 
    }
}
