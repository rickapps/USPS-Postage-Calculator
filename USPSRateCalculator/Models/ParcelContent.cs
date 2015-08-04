//----------------------------------------------------------------------- 
// <copyright file="ParcelContent.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Models
{
    using RickApps.USPSRateCalculator.Interfaces;

    public class ParcelContent : IParcelContent
    {
        public string ID { get; set; }
        public int? Pounds { get; set; }
        public int? Ounces { get; set; }
        public string ContainerType { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Girth { get; set; }
        public bool IsOverSize
        {
            get 
            {
                bool isOversize = (Height.HasValue && Height.Value > 12.0);
                if (!isOversize)
                {
                    isOversize = (Width.HasValue && Width.Value > 12.0);
                }
                if (!isOversize)
                {
                    isOversize = (Length.HasValue && Length.Value > 12.0);
                }

                return isOversize;
            }
        }

        public bool IsOddShape 
        {
            get
            {
                return (Girth.HasValue && Girth > 12.0);
            }
        }
    }
}
