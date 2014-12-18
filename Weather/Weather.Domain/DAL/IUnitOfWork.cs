using System;
using Weather.Domain.Entities;
namespace Weather.Domain.DAL
{
    interface IUnitOfWork
    {
        IGenericRepository<Forecast> ForecastRepository { get; }
        IGenericRepository<Location> LocationRepository { get; }
    }
}
