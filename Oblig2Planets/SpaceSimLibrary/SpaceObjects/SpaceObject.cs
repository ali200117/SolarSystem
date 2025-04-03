using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaceSimLibrary.SpaceObjects

{
    public enum SpaceObjectType
    {
        Star,
        Planet,
        Moon,
        Comet,
        Astroid,
        DwarfPlanet
    }


    public class SpaceObject
    {
        public string Name { get; set; }
        public SpaceObjectType ObjectType { get; set; }
        public string Orbits { get; set; }
        public double Distance { get; set; }
        public double OrbitalPeriod { get; set; }
        public string Color { get; set; }
        public double RotationalPeriod { get; set; }  

        
        public SpaceObject(string name, SpaceObjectType objectType, string orbits, double distance, double orbitalPeriod, string color, double rotationalPeriod)
        {
            Name = name;
            ObjectType = objectType;
            Orbits = orbits;
            Distance = distance;
            OrbitalPeriod = orbitalPeriod;
            Color = color;
            RotationalPeriod = rotationalPeriod; 
        }

        public void CalculatePosition (double time)
        {
            double angle = (2 * Math.PI * time) / OrbitalPeriod;
            double x = Distance * Math.Cos(angle);
            double y = Distance * Math.Sin(angle);

            Console.WriteLine($"Position of {Name} at time: {time}: X = {x}, Y = {y}, Color: {Color}");
        }
    }
}
