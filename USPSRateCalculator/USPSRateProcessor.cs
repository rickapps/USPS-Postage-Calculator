//----------------------------------------------------------------------- 
// <copyright file="USPSRateProcessor.cs" company="StellerJay Enterprises, LLC"> 
// Copyright (c) Rick Eichhorn   
// <author>Rick Eichhorn</author> 
// <date>Friday, March 27, 2015 7:24:51 PM</date> 
// </copyright> 
//-----------------------------------------------------------------------

namespace RickApps.USPSRateCalculator
{
    using RickApps.USPSRateCalculator.Interfaces;
    using RickApps.USPSRateCalculator.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Xml.Linq;

    public class USPSRateProcessor
    {
        private string _userID;
        private int _originationZIP;
        private XElement _lastRequest;
        private XElement _lastResponse;
        private const string _requestURL = "http://production.shippingapis.com";

        /// <summary>
        /// Constructor to set our constant values
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="originationZIP"></param>
        public USPSRateProcessor(string userID, int originationZIP)
        {
            _userID = userID;
            _originationZIP = originationZIP;
        }

        public XElement LastRequest
        {
            get { return _lastRequest; }
        }

        public XElement LastResponse
        {
            get { return _lastResponse; }
        }

        /// <summary>
        /// Create an XML request to send to USPS.
        /// </summary>
        /// <param name="parcel"></param>
        /// <returns></returns>
        private XElement GenerateRequest(IParcel parcel)
        {
            // Create an XML request from the values in package
            XElement request = new XElement("RateV4Request");
            request.SetAttributeValue("USERID", _userID);
            XElement revision = new XElement("Revision", "2");
            request.Add(revision);

            XElement package = new XElement("Package");
            package.SetAttributeValue("ID", "1");

            XElement serviceType = new XElement("Service", parcel.ShipMethod);
            package.Add(serviceType);

            // If we use a First Class service, we must include a type
            if (parcel.ShipMethod == "FIRST CLASS")
            {
                XElement firstClassType = new XElement("FirstClassMailType", parcel.ContainerType);
                package.Add(firstClassType);
            }

            XElement origin = new XElement("ZipOrigination", _originationZIP);
            package.Add(origin);

            XElement destinationZIP = new XElement("ZipDestination", parcel.DestinationZip);
            package.Add(destinationZIP);

            XElement pounds = new XElement("Pounds", parcel.Pounds);
            package.Add(pounds);

            XElement ounces = new XElement("Ounces", parcel.Ounces);
            package.Add(ounces);

            if (parcel.IsOverSize)
            {
                // Is the package rectangular?
                string shape = parcel.IsOddShape ? "NONRECTANGULAR" : "RECTANGULAR";
                XElement container = new XElement("Container", shape);
                package.Add(container);
                XElement size = new XElement("Size", "LARGE");
                package.Add(size);

                // We need our dimensions
                XElement width = new XElement("Width", parcel.Width);
                package.Add(width);

                XElement length = new XElement("Length", parcel.Length);
                package.Add(length);

                XElement height = new XElement("Height", parcel.Height);
                package.Add(height);

                if (parcel.IsOddShape)
                {
                    XElement girth = new XElement("Girth", parcel.Girth);
                    package.Add(girth);
                }
            }
            else
            {
                XElement container = new XElement("Container");
                package.Add(container);
                XElement size = new XElement("Size", "REGULAR");
                package.Add(size);
            }

            XElement value = new XElement("Value", "1500");
            package.Add(value);

            XElement machinable = new XElement("Machinable", parcel.IsMachinable);
            package.Add(machinable);

            //XElement services = new XElement("SpecialServices");
            //XElement special = new XElement("SpecialService", "1");
            //services.Add(special);
            //package.Add(services);

            request.Add(package);

            return request;
            
        }

        /// <summary>
        /// Create a list of rates for each parcel sent in our request.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private IEnumerable<IParcelRate> ParseResponse(XElement response)
        {
            List<IParcelRate> rateCollection = new List<IParcelRate>();
            IEnumerable<XElement> packages = response.Elements();

            if (response.Name.LocalName.Equals("Error", StringComparison.InvariantCultureIgnoreCase))
            {
                string errNum = response.Element("Number").Value;
                string errSource = response.Element("Source").Value;
                string errDesc = response.Element("Description").Value;
                throw new ApplicationException(string.Format("Error: {0} - {1} ({2})", errNum, errDesc, errSource));
            }

            foreach (var package in packages)
            {
                rateCollection.Add(ParseParcel(package));
            }
            return rateCollection;

        }

        /// <summary>
        /// Obtain the rates for a single parcel.
        /// </summary>
        /// <param name="package"></param>
        /// <returns></returns>
        private IParcelRate ParseParcel(XElement package)
        {
            int intValue;
            double doubleValue;

            ParcelContent parcel = new ParcelContent();
            parcel.ID = package.Attribute("ID").Value;
            // Check for an error
            if (package.Element("Error") != null)
            {
                XElement error = package.Element("Error");
                string errNum = error.Element("Number").Value;
                string errSource = error.Element("Source").Value;
                string errDesc = error.Element("Description").Value;
                throw new ApplicationException(string.Format("Error: {0} - {1} ({2})", errNum, errDesc, errSource));
            }

            Int32.TryParse(package.Element("Pounds").Value, out intValue);
            parcel.Pounds = intValue;
            Int32.TryParse(package.Element("Ounces").Value, out intValue);
            parcel.Ounces = intValue;

            ParcelRates rates = new ParcelRates(parcel);
            Int32.TryParse(package.Element("ZipOrigination").Value, out intValue);
            rates.ZipOrigination = intValue;
            Int32.TryParse(package.Element("ZipDestination").Value, out intValue);
            rates.ZipDestination = intValue;

            if (package.Element("FirstClassMailType") != null)
            {
                parcel.ContainerType = package.Element("FirstClassMailType").Value;
            }
            if (package.Element("Container") != null)
            {
                parcel.ContainerType = package.Element("Container").Value;
            }
            if (package.Element("Width") != null && double.TryParse(package.Element("Width").Value, out doubleValue))
            {
                parcel.Width = doubleValue;
            }
            if (package.Element("Length") != null && double.TryParse(package.Element("Length").Value, out doubleValue))
            {
                parcel.Length = doubleValue;
            }
            if (package.Element("Height") != null && double.TryParse(package.Element("Height").Value, out doubleValue))
            {
                parcel.Height = doubleValue;
            }
            if (package.Element("Girth") != null && double.TryParse(package.Element("Girth").Value, out doubleValue))
            {
                parcel.Girth = doubleValue;
            }

            if (package.Element("Machinable") != null)
            {
                rates.IsMachinable = package.Element("Machinable").Value.Equals("TRUE");
            }

            if (Int32.TryParse(package.Element("Zone").Value, out intValue))
            {
                rates.Zone = intValue;
            }

            IEnumerable<XElement> postageCollection = package.Elements("Postage");
            foreach (var postage in postageCollection)
            {
                rates.AddRate(ParsePostage(postage));
            }

            return rates;
        }

        private IPostage ParsePostage(XElement rate)
        {
            double doubleValue;
            Postage postage = new Postage();
            postage.ID = rate.Attribute("CLASSID").Value;
            postage.Service = WebUtility.HtmlDecode(rate.Element("MailService").Value);
            if (double.TryParse(rate.Element("Rate").Value, out doubleValue))
            {
                postage.Rate = doubleValue;
            }
            if (rate.Element("Commitment") != null)
            {
                postage.CommitName = rate.Element("Commitment").Value;
            }
            if (rate.Element("CommitmentDate") != null)
            {
                postage.CommitDate = rate.Element("CommitmentDate").Value;
            }

            // Check for special services
            IEnumerable<XElement> serviceCollection = new List<XElement>();
            XElement serviceSection = rate.Element("SpecialServices");
            if (serviceSection != null)
            {
                serviceCollection = serviceSection.Elements();
            }
            foreach (var service in serviceCollection)
            {
                SpecialService special = new SpecialService();
                special.ID = service.Element("ServiceID").Value;
                special.Name = WebUtility.HtmlDecode(service.Element("ServiceName").Value);
                special.Available = service.Element("Available").Value.Equals("TRUE");
                if (double.TryParse(service.Element("Price").Value, out doubleValue))
                {
                    special.Rate = doubleValue;
                }
                postage.AddService(special);
            }

            return postage;
        }

        private XElement SendRequest(XElement request)
        {
            // Obtain our rates
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_requestURL);
                var values = new Dictionary<string, string>
                    {
                       {"API", "RateV4"},
                       {"XML", request.ToString()}
                    };

                var content = new FormUrlEncodedContent(values);
                var response = client.PostAsync("/ShippingAPI.dll", content).Result;
                var stream = response.Content.ReadAsStreamAsync().Result;
                return XElement.Load(stream);
            }

        }

        public IEnumerable<IParcelRate> GetRates(IParcel package)
        {
            _lastRequest = GenerateRequest(package);
            _lastResponse = SendRequest(_lastRequest);
            return ParseResponse(_lastResponse);
        }
    }
}
