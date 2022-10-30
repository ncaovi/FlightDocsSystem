using System;
using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Model
{
    public class CargoManifest
    {
        [Key]
        public int FlightId { get; set; }
        public int FlightNo { get; set; }
        public DateTime Time { get; set; }
        public string PointOfUnLoading { get; set; }
        public string PointOfLoading { get; set; }
    }
}
