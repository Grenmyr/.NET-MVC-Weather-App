using System;
using Weather.Domain.Entities;
namespace Weather.Domain.DAL
{
    public interface IUnitOfWork
    {
        IGenericRepository<Forecast> ForecastRepository { get; }
        IGenericRepository<Location> LocationRepository { get; }

         void Save();
    }
}
