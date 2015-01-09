using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.DAL;
using Weather.Domain.Entities;
using Weather.Domain.Validation;
using Weather.Domain.Webservices;

namespace Weather.Domain.Service
{
    public class WeatherService 
    {
        private IUnitOfWork _unitOfWork;
        private GeoNamesWebservice _geoNamesWebservice;
        private YrWebservice _yrWebservice;
        private DbContextDataAnotationValidation _dbContextDataAnotationValidation;

        public WeatherService()
            : this(new UnitOfWork(), new GeoNamesWebservice(), new YrWebservice(), new DbContextDataAnotationValidation())
        {

        }      

        public WeatherService(IUnitOfWork unitOfWork,
            GeoNamesWebservice geoNamesWebservice,
            YrWebservice yrWebservice,
            DbContextDataAnotationValidation dbContextDataAnotationValidation
            )
        {
            _unitOfWork = unitOfWork;
            _geoNamesWebservice = geoNamesWebservice;
            _yrWebservice = yrWebservice;
            _dbContextDataAnotationValidation = dbContextDataAnotationValidation;
        }

        public IEnumerable<Location> GetLocation(string search)
        {
            search = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(search);
            var locations = _unitOfWork.LocationRepository.Get(l => l.Name == search) as IEnumerable<Location>;

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


                return location.Forecasts;
            }
            else
            {
         
                return location.Forecasts;
            }

        }

        public void RefreshForecasts(Location location)
        {
            foreach (var forecast in location.Forecasts.ToList())
            {
                _unitOfWork.ForecastRepository.Remove(forecast.Id);
                
            }
            var forecasts = _yrWebservice.GetForecasts(location);

            // Todo implement validation before insertion
            //_dbContextDataAnotationValidation.TryValidate();

            foreach (var forecast in forecasts)
            {
                _unitOfWork.ForecastRepository.Add(forecast);
              
            }           
        }
        
    
    }


}
