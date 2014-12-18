using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Entities;

namespace Weather.Domain.DAL
{
    class UnitOfWork : IUnitOfWork
    {
        private DbContext _context = new WeatherContext();
        private IGenericRepository<Location> _locationRepository;
        private IGenericRepository<Forecast> _forecastRepository;

        public IGenericRepository<Location> LocationRepository
        {
            get { return _locationRepository ?? (_locationRepository = new GenericRepository<Location>(_context));}
        }
        public IGenericRepository<Forecast> ForecastRepository
        {
            get { return _forecastRepository ?? (_forecastRepository = new GenericRepository<Forecast>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
