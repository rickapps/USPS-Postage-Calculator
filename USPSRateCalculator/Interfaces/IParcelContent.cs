//----------------------------------------------------------------------- 
// <copyright file="IParcelContent.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------


namespace RickApps.USPSRateCalculator.Interfaces
{
    /// <summary>
    /// Represents an item that can be added to a parcel. A parcel can contain a single parcel content. In this case, the weight and size represented here
    /// also is the weight and size of the entire parcel.
    /// </summary>
    public interface IParcelContent
    {
        string ID { get; }
        string ContainerType { get; }
        int? Pounds { get; }
        int? Ounces { get; }
        bool IsOverSize { get; }
        bool IsOddShape { get; }
        double? Height { get; }
        double? Length { get; }
        double? Width { get; }
        double? Girth { get; }
    }
}
