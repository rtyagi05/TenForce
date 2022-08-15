using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }
        public double AverageMoonGravity
        { get; set; }

        public Planet(PlanetDto planetDto)
        {
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if (planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
                CalculateAverageMoonGravity();
            }
          
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
        private void CalculateAverageMoonGravity()
        {
           
            //adding this check because this will generate invalid data 
            if (SemiMajorAxis> 0)
            {
                try
                {
                    //Moon Gravity 
                    //g=(G*M)/R*R
                    //Average moon gravity
                    //g=(G*(M1+M2+....+Mn))/(R*R*n)
                    //where n is the number of moons
                    //G = 6.674×10-11 m3kg-1s-2
                    double averageMass = CalculateAverageMassOfMoon();
                    //calculate gravity
                    //g=G*AverageMass
                    AverageMoonGravity = (6.674 * Math.Pow(10, -11)) * averageMass;
                    //g=(G*averageMass)/R*R
                    AverageMoonGravity = AverageMoonGravity / Math.Pow(SemiMajorAxis, 2);
                }
                catch (ArithmeticException exp)
                {
                    Console.WriteLine(exp.Message);

                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);

                }
            }
            else
            {
                AverageMoonGravity = 0.0f;
            }
           
        }
        private double CalculateAverageMassOfMoon()
        {
            double totalMass=0;
            //calculate M1+M2+....+Mn
            foreach (Moon moon in Moons)
            {
                totalMass= totalMass+((moon.MassValue) * Math.Pow(10, moon.MassExponent));
            }
            //average mass
            return (totalMass / Moons.Count);

        }
    }
}
