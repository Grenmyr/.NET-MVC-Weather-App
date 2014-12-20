namespace Weather.Domain.Entities
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        public Location(JToken location)
            : this()
        {
            AdminName1 = location.Value<string>("adminName1");
            CountryName = location.Value<string>("countryName");     
            Lat = location.Value<string>("lat");
            Lng = location.Value<string>("lng");
            Name = location.Value<string>("name");         
            Timestamp = DateTime.Now.AddHours(1);
        }
    }

    [Table("appSchema.Location")]
    public partial class Location
    {

        public Location()
        {
            this.Forecasts = new HashSet<Forecast>();
        }
       

        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Lat { get; set; }

        [Required]
        [StringLength(25)]
        public string Lng { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string AdminName1 { get; set; }

        [Required]
        [StringLength(50)]
        public string CountryName { get; set; }


        public virtual ICollection<Forecast> Forecasts { get; set; }
    }
}
