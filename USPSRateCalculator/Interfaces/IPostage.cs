//----------------------------------------------------------------------- 
// <copyright file="IPostage.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// A rate for a specified shipping method. The information is returned by the USPS rate calculator. Each shipping method can also include
    /// the rates for optional special services like insurance, delivery confirmation, etc.
    /// </summary>
    public interface IPostage
    {
        string ID { get; }
        string Service { get; }
        double Rate { get; }
        string CommitName { get; }
        string CommitDate { get; }
        IEnumerable<ISpecialService> Services { get; }
    }
}
