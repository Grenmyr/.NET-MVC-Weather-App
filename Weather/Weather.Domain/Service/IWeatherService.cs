using System;
using System.Collections.Generic;
using Weather.Domain.Entities;
namespace Weather.Domain.Service
{
    public interface IWeatherService
    {
        IEnumerable<Forecast> GetForecast(Location location);
        IEnumerable<Location> GetLocation(string search);
        Location GetLocationById(int id);
        void RefreshForecasts(Location location);
    }
}
