using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService2.Models
{
    public class PlaneGeoLocation
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
    }
}
