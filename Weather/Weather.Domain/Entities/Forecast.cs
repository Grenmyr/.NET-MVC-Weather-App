namespace Weather.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Collections;
    using System.Xml.Linq;


    public partial class Forecast
    {
        private IEnumerable<Forecast> forecasts;
        public IEnumerable<Forecast> Forecasts
        {
            get { return forecasts != null ? forecasts : null; }
        }

        public Forecast ()
        {

        }
    }



    [Table("appSchema.Forecast")]
    public partial class Forecast
    {
        public int Id { get; set; }

        [StringLength(4)]
        public string Temperature { get; set; }

        [StringLength(6)]
        public string Cloudiness { get; set; }

        [StringLength(6)]
        public string NederBird { get; set; }

        [Required]
        public int SymbolId { get; set; }

        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
