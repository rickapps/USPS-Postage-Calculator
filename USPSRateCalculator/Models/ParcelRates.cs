//----------------------------------------------------------------------- 
// <copyright file="ParcelRates.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator.Models
{
    using RickApps.USPSRateCalculator.Interfaces;
    using System.Collections.Generic;

    public class ParcelRates : IParcelRate
    {
        private IParcelContent _parcel;
        private List<IPostage> _rates;

        public ParcelRates(ParcelContent parcel)
        {
            _parcel = parcel;
            _rates = new List<IPostage>();
        }

        public int ZipOrigination {get; set;}

        public int ZipDestination {get; set;}

        public bool IsMachinable {get; set;}

        public int Zone {get; set;}

        public IEnumerable<IPostage> RateCollection
        {
            get { return _rates; }
        }

        public string ID
        {
            get
            {
                return _parcel.ID;
            }
        }

        public string ContainerType
        {
            get
            {
                return _parcel.ContainerType;
            }
        }

        public int? Pounds
        {
            get
            {
                return _parcel.Pounds;
            }
        }

        public int? Ounces
        {
            get
            {
                return _parcel.Ounces;
            }
        }

        public bool IsOverSize
        {
            get { return _parcel.IsOverSize; }
        }

        public bool IsOddShape
        {
            get { return _parcel.IsOddShape; }
        }

        public double? Height
        {
            get
            {
                return _parcel.Height;
            }
        }

        public double? Length
        {
            get
            {
                return _parcel.Length;
            }
        }

        public double? Width
        {
            get
            {
                return _parcel.Width;
            }
        }

        public double? Girth
        {
            get
            {
                return _parcel.Girth;
            }
        }

        public void AddRate(IPostage postage)
        {
            _rates.Add(postage);
        }
    }
}
