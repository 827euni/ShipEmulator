using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipEmulator.Models
{
    internal class GPS
    {
        public long GPS_ID { get; set; }
        public DateTime GPS_TIME { get; set; }
        public Decimal GPS_Latitude { get; set; }
        public Decimal GPS_Longitude { get; set; }
    }
}
