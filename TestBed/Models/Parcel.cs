using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RickApps.USPSRateCalculator.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RickApps.TestBed.Models
{
    public class Parcel : IParcel
    {
        private List<IParcelContent> _parcelCollection;
        private List<Tuple<string, string>> _shippingMethods;
        private List<Tuple<string, string>> _mailTypes;

        public Parcel()
        {
            IsMachinable = true;
        }

        [Required(ErrorMessage = "Please enter the zip code of where the package is being delivered")]
        public int? DestinationZip { get; set; }

        [Required(ErrorMessage = "Enter the weight of the package in pounds")]
        public double? Pounds {get; set;}

        [Required(ErrorMessage = "Enter the weight of the package in ounces")]
        public double? Ounces {get; set;}
        public double? Height {get; set;}
        public double? Length {get; set;}
        public double? Width {get; set;} 
        public double? Girth {get; set;}
        public string RateResponse { get; set; }
        public IEnumerable<Tuple<string, string>> Methods
        {
            get
            {
                if (_shippingMethods == null)
                {
                    _shippingMethods = new List<Tuple<string, string>>();
                    _shippingMethods.Add(new Tuple<string, string>("First Class", "FIRST CLASS"));
                    _shippingMethods.Add(new Tuple<string, string>("Priority", "PRIORITY"));
                    _shippingMethods.Add(new Tuple<string, string>("Priority Express", "PRIORITY MAIL EXPRESS"));
                    _shippingMethods.Add(new Tuple<string, string>("Standard Post", "STANDARD POST"));
                    _shippingMethods.Add(new Tuple<string, string>("Media", "MEDIA"));
                }

                return _shippingMethods;

            }
        }

        public IEnumerable<Tuple<string, string>> MailTypes
        {
            get
            {
                if (_mailTypes == null)
                {
                    _mailTypes = new List<Tuple<string, string>>();
                    _mailTypes.Add(new Tuple<string, string>("Letter", "LETTER"));
                    _mailTypes.Add(new Tuple<string, string>("Flat", "FLAT"));
                    _mailTypes.Add(new Tuple<string, string>("Parcel", "PARCEL"));
                    _mailTypes.Add(new Tuple<string, string>("Postcard", "POSTCARD"));
                    _mailTypes.Add(new Tuple<string, string>("Package Service", "PACKAGE SERVICE"));
                }
                return _mailTypes;
            }
        }

        public string ID {get; set;}

        public string ContainerType {get; set; }

        public bool IsOverSize {get; set;}

        public bool IsOddShape {get; set;}

        public bool IsMachinable { get; set; }

        public IEnumerable<IParcelContent> ContentCollection
        {
            get { return _parcelCollection; }
        }

        public string ShipMethod {get; set; }

        public string Address {get; set; }
    }
}