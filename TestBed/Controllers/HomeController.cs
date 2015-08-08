using RickApps.TestBed.Models;
using RickApps.USPSRateCalculator;
using RickApps.USPSRateCalculator.Interfaces;
using RickApps.USPSRateCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RickApps.TestBed.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Parcel myBox = new Parcel();
            return View(myBox);
        }

        [HttpPost]
        public ActionResult Index(Parcel package)
        {
            ViewBag.Message = "Testing - Postage Calculation.";
            if (!ModelState.IsValid)
            {
                // Let the user correct their error.
                return View(package);
            }

            // Obtain results from USPS and display them
            int origin;
            string userID = @System.Configuration.ConfigurationManager.AppSettings["userID"];
            Int32.TryParse(@System.Configuration.ConfigurationManager.AppSettings["originZIP"], out origin);
            USPSRateProcessor xmlProcessor = new USPSRateProcessor(userID, origin);
            try
            {
                IEnumerable<IParcelRate> rates = xmlProcessor.GetRates(package);
                ParcelRates myRate = (ParcelRates)rates.ElementAt(0);
                Postage myPostage = (Postage)myRate.RateCollection.ElementAt(0);
                TempData["Rates"] = rates;
                return this.RedirectToAction("ShowResults");
                //package.RateResponse = xmlProcessor.LastResponse.ToString();
                //return View(package);
            }
            catch (ApplicationException e)
            {
                ViewBag.Message = string.Format("You need to edit Web.config -- {0}", e.Message);
                return View(package);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public ActionResult ShowResults()
        {
            IEnumerable<IParcelRate> rates = (IEnumerable<IParcelRate>)TempData["Rates"];
            return View(rates);
        }
        public ActionResult About()
        {
            ViewBag.Message = "USPS Postage Rate Calculator.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "RickApps";

            return View();
        }
    }
}