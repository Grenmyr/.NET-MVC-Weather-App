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
            : this(new UnitOfWork(), new GeoNamesWebservice())
        {

        }

        public WeatherService(IUnitOfWork unitOfWork, GeoNamesWebservice geoNamesWebservice)
        {
            _unitOfWork = unitOfWork;
            _geoNamesWebservice = geoNamesWebservice;
        }

        public IEnumerable< Location> getLocation(string search)
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

    }
}
