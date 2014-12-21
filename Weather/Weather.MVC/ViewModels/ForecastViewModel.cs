using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weather.Domain.Entities;

namespace Weather.MVC.ViewModels
{
    public class ForecastViewModel
    {

        public Location location { get; set; }

        public IEnumerable<Forecast> forecasts
        {
            get { return location != null ? location.Forecasts : null; }
        }

    }
}