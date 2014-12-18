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
    class WeatherService
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

        public Location getLocation(string search)
        {
            var location = _unitOfWork.LocationRepository.Get(l => l.Name == search);

            return new Location();
        }

    }
}
