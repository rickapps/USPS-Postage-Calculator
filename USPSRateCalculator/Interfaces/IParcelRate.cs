//----------------------------------------------------------------------- 
// <copyright file="IParcelRates.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// The collection of rates returned by the USPS rate calculator for a single parcel.
    /// </summary>
    public interface IParcelRate : IParcelContent
    {
        int ZipOrigination { get; }
        int ZipDestination { get; }
        bool IsMachinable { get; }

        int Zone { get; }
        IEnumerable<IPostage> RateCollection { get; }
    }
}
