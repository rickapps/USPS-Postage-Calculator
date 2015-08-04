//----------------------------------------------------------------------- 
// <copyright file="ISpecialService.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------


namespace RickApps.USPSRateCalculator.Interfaces
{

    /// <summary>
    /// The rate for a special service that can be added to a specific shipping method. The service and rate is returned by the USPS rate calculator.
    /// </summary>
    public interface ISpecialService
    {
        string ID { get; }
        string Name { get; }
        bool Available { get; }
        double Rate { get; }
    }
}
