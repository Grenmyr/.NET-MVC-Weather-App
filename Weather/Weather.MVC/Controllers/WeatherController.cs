using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain;
using Weather.Domain.Entities;
using Weather.Domain.Service;
using Weather.Domain.Webservices;
namespace Weather.MVC.Controllers
{
    public class WeatherController : Controller
    {
        private WeatherService _service;
       
        public WeatherController()
            : this(new WeatherService())
        {

        }
        public WeatherController(WeatherService weatherservice)
        {
            _service = weatherservice;
        }
        // GET: Weather
        public ActionResult Index()
        {
            return View("index");
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include="Name")]Location location)
        {
            //var date = DateTime.Now.AddHours(1);
            //var newdate = DateTime.Now;   
            //var hours = (date - newdate).TotalHours;
            //var location = new Location();
            //location.Lat = "56";
            //location.Lng = "16";

            //var forecast = new YrWebservice();
            //forecast.GetForecast(location);

            //var search = new GeoNamesWebservice();
            //var list = search.FindLocation("Kalmar");
            var locations = _service.getLocation("Västervik");

            return View("Locations",locations);
        }
       
    }
}