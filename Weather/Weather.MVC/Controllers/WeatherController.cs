using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain;
using Weather.Domain.Entities;
using Weather.Domain.Webservices;
namespace Weather.MVC.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather
        public ActionResult Index()
        {
            var date = DateTime.Now.AddHours(1);

            //var newdate = DateTime.Now;

            
            //var hours = (date - newdate).TotalHours;



            //var location = new Location();
            //location.Lat = "56";
            //location.Lng = "16";

            //var forecast = new YrWebservice();
            //forecast.GetForecast(location);

            //var search = new GeoNamesWebservice();
            //var list = search.FindLocation("Kalmar");

            return View();
        }
    }
}