//----------------------------------------------------------------------- 
// <copyright file="SpecialService.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------


using RickApps.USPSRateCalculator.Interfaces;
namespace RickApps.USPSRateCalculator.Models
{
    public class SpecialService : ISpecialService
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Available {get; set; }
        public double Rate { get; set; }
    }
}
