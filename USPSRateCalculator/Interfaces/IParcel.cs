//----------------------------------------------------------------------- 
// <copyright file="IParcel.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// A parcel can contain many parcel content items. The parcel represents the addressed package. It contains all the information required for a rate request.
    /// </summary>
    public interface IParcel 
    {
        IEnumerable<IParcelContent> ContentCollection { get; }
        string ShipMethod { get; }
        string ContainerType { get; }
        bool IsMachinable { get; }
        string Address { get; }
        int? DestinationZip { get; }
        double? Pounds { get; }
        double? Ounces { get; }
        bool IsOverSize { get; }
        bool IsOddShape { get; }
        double? Height { get; }
        double? Length { get; }
        double? Width { get; }
        double? Girth { get; }
    }
}
