namespace Weather.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("appSchema.Forecast")]
    public partial class Forecast
    {
        public int Id { get; set; }

        public int Temperature { get; set; }

        public int CloudFactor { get; set; }

        public int NederBird { get; set; }

        [Required]
        [StringLength(50)]
        public string SymbolId { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
