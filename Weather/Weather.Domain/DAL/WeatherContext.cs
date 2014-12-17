namespace Weather.Domain.DAL
{
    using Weather.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WeatherContext : DbContext
    {
        public WeatherContext()
            : base("name=WeatherDomain")
        {
        }

        public virtual DbSet<Forecast> Forecasts { get; set; }
        public virtual DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
