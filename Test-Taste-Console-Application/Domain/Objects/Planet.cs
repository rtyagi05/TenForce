using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Test_Taste_Console_Application.Domain.DataTransferObjects;
using Test_Taste_Console_Application.Domain.Services;

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
                AverageMoonGravity= PlanetService.CalculateAverageMoonGravity(Moons,SemiMajorAxis);
            }
          
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }
        
    }
}
