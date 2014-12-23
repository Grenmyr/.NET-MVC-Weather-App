using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

                        return String.Format("Most recent forecast {0}:00 today.", DateTime.Now.Hour);
                    case 2:
                        return "Tomorrow 12:00";
                    case 3:
                        return String.Format("{0} 12:00", DateTime.Today.AddDays(2).DayOfWeek.ToString());
                    case 4:
                        return String.Format("{0} 12:00", DateTime.Today.AddDays(3).DayOfWeek.ToString());
                    case 5:
                        return String.Format("{0} 12:00", DateTime.Today.AddDays(4).DayOfWeek.ToString());
                    default:
                        return String.Format("{0} 12:00", DateTime.Today.AddDays(5).DayOfWeek.ToString());
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
        public bool GotLocations
        {
            get { return Locations != null && Locations.Any(); }
        }

        public IEnumerable<Forecast> Forecasts
        {
            get { return Location != null ? Location.Forecasts : null; }
            set { }
        }
        public Location Location { get; set; }

        [DisplayName("City or village")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public IEnumerable<Location> Locations { get; set; }


    }
}