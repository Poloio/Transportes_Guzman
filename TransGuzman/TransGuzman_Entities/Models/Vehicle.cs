using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Vehicle
    {
        public string VehicleID { get; set; }
        public string ModelName { get; set; }
        public string RequiredDriverLicenseID { get; set; }
        public int? MaxCargo { get; set; }

        public LicenseType LicenseType { get; set; }
    }
}
