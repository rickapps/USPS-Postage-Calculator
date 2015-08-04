//----------------------------------------------------------------------- 
// <copyright file="Postage.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Models
{
    using RickApps.USPSRateCalculator.Interfaces;
    using System.Collections.Generic;

    public class Postage : IPostage
    {
        private List<ISpecialService> _services;

        public Postage()
        {
            _services = new List<ISpecialService>();
        }
        public string ID { get; set; }
        public string Service { get; set; }
        public double Rate { get; set; }
        public string CommitName { get; set; }
        public string CommitDate { get; set; }
        public IEnumerable<ISpecialService> Services
        {
            get { return _services; }
        }
        public void AddService(ISpecialService service)
        {
            _services.Add(service);
        }
    }
}
