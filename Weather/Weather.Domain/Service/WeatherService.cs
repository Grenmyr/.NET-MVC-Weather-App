using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.DAL;
using Weather.Domain.Entities;
using Weather.Domain.Webservices;

namespace Weather.Domain.Service
{
    public class WeatherService
    {
        private IUnitOfWork _unitOfWork;
        private GeoNamesWebservice _geoNamesWebservice;
        private YrWebservice _yrWebservice;

        public WeatherService()
            : this(new UnitOfWork(), new GeoNamesWebservice(), new YrWebservice())
        {

        }

        public WeatherService(IUnitOfWork unitOfWork, GeoNamesWebservice geoNamesWebservice, YrWebservice yrWebservice)
        {
            _unitOfWork = unitOfWork;
            _geoNamesWebservice = geoNamesWebservice;
            _yrWebservice = yrWebservice;
        }

        public IEnumerable<Location> GetLocation(string search)
        {
            var locations = _unitOfWork.LocationRepository.Get(l => l.Name == search);

            //TODO IMPLEMENT TIMESTAMP
            if (locations.Any())
            {
                return locations;
            }

            locations = _geoNamesWebservice.FindLocation(search);

            var relevantList = locations.Select(l => l).Where(l => l.Name == search).ToList();

            foreach (var location in relevantList)
            {
                _unitOfWork.LocationRepository.Add(location);
                _unitOfWork.Save();
            }
            return relevantList;
        }

        public Location GetLocationById(int id)
        {
            var selectedLocation = _unitOfWork.LocationRepository.GetById(id);

            return selectedLocation;
        }

        public IEnumerable<Forecast> GetForecast(Location location)
        {
            if (!location.Forecasts.Any() || (location.Timestamp - DateTime.Now).TotalHours <= 0)
            {
                RefreshForecasts(location);

                location.Timestamp = DateTime.Now.AddHours(1);
                _unitOfWork.LocationRepository.Update(location);
                _unitOfWork.Save();
            }
            else
            {
                var forecast = location.Forecasts;
            }

            //var forecasts = _yrWebservice.GetForecast(id);
            throw new NotImplementedException();
        }

        public void RefreshForecasts(Location location)
        {

            foreach (var forecast in location.Forecasts.ToList())
            {
                _unitOfWork.ForecastRepository.Remove(forecast.Id);
                
            }
            var forecasts = _yrWebservice.GetForecasts(location);

            foreach (var forecast in forecasts)
            {
                _unitOfWork.ForecastRepository.Add(forecast);
                
            }           
        }
    }
}
