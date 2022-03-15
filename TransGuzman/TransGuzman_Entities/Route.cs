using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities
{
    public class Route
    {
        public int Number { get; set; }
        public string DriverID { get; set; }
        public string TruckLicenseNumber { get; set; }
        public int OriginCode { get; set; }
        public int DestinationCode { get; set; }
        public int Kilometers { get; set; }
    }
}
