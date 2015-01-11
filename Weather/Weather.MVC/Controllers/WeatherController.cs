using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private IWeatherService _service;

        private ForecastViewModel forecastViewModel = new ForecastViewModel();

        public WeatherController()
            : this(new WeatherService())
        {
            // om bara en träff visa forecast.
        }
        public WeatherController(IWeatherService weatherservice)
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
        public ActionResult Index([Bind(Include = "Name")]ForecastViewModel forecastViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO keep tryupdate or not use it?
                    TryUpdateModel(forecastViewModel.Locations = _service.GetLocation(forecastViewModel.Name));
                }
                else
                {
                    return View("index");
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

                return View("index");
            }

            if (forecastViewModel.Locations.Count() == 1)
            {
                var id = forecastViewModel.Locations.Select(l => l.Id).First();
                return RedirectToAction("Forecast", new { id });

            }

            return View("Locations", forecastViewModel);


        }
        //validation? 
        public ActionResult Forecast(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    forecastViewModel.Location = _service.GetLocationById(id);

                    if (forecastViewModel.Location != null)
                    {
                        forecastViewModel.Forecasts = _service.GetForecast(forecastViewModel.Location);
                    }
                    else
                    {
                        return new HttpNotFoundResult();
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