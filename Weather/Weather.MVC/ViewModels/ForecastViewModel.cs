using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weather.Domain.Entities;

namespace Weather.MVC.ViewModels
{
    public class ForecastViewModel
    {
        private int count = 0;
        public string IconUrl
        {
            get { return "http://api.yr.no/weatherapi/weathericon/1.1/?symbol={0};content_type=image/png"; }
        }


        public string PresentDay
        {
            get
            {
                switch (Count)
                {
                    case 1:
                        return DateTime.Today.ToShortDateString();
                    case 2:
                        return DateTime.Today.AddDays(1).ToShortDateString();
                    case 3:
                        return DateTime.Today.AddDays(2).ToShortDateString();
                    case 4:
                        return DateTime.Today.AddDays(3).ToShortDateString();
                    case 5:
                        return DateTime.Today.AddDays(4).ToShortDateString();
                    default:
                        return DateTime.Today.AddDays(1).ToShortDateString();
                }
            }  
            
        }

        public int Count 
        {
            get { if (count >= 5) { count = 0; } count++; return count; }
        }


        public bool GotForecasts
        {
            get { return Forecasts != null && Forecasts.Any(); }
        }

        public IEnumerable<Forecast> Forecasts
        {
            get { return Location != null ? Location.Forecasts : null; }
            set { }
        }
        public Location Location { get; set; }
    }
}