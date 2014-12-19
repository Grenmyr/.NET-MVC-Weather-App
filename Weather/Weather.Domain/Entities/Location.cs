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
            Name = location.Value<string>("name");
            Lat = location.Value<string>("lat");
            Lng = location.Value<string>("lng");
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
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public int geonameId { get; set; }

        public virtual ICollection<Forecast> Forecasts { get; set; }
    }
}
