using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Domain;
using Weather.Domain.Entities;
using Weather.Domain.Service;
using Weather.Domain.Webservices;
using Weather.MVC.ViewModels;
namespace Weather.MVC.Controllers
{
    public class WeatherController : Controller
    {
        private WeatherService _service;
        private IEnumerable<Location> locations;
        private IEnumerable<Forecast> forecasts;

        private ForecastViewModel forecastViewModel = new ForecastViewModel();

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
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name")]Location location)
        {
            try
            {
                // TODO keep tryupdate or not use it?
                TryUpdateModel(locations = _service.GetLocation(location.Name));
                         
            }
            catch (Exception ex)
            {
                // Bubble downish to fetch error and write it.
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View("Locations", locations);
        }

        // TODO bind ID? validation? Errorhandling?
        public ActionResult Forecast(int id)
        {           
            try
            {
                if (ModelState.IsValid)
                {
                    forecastViewModel.Location = _service.GetLocationById(id);
                    //var location = _service.GetLocationById(id);

                    if (forecastViewModel.Location != null)
                    {
                        //forecasts = _service.GetForecast(location);
                        forecastViewModel.Forecasts = _service.GetForecast(forecastViewModel.Location);
                    }
                }
            }
            catch (Exception ex)
            {
                // Bubble downish to fetch error and write it.
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return View(forecastViewModel);
        }



    }
}