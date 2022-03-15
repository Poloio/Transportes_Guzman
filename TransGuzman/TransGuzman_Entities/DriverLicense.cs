using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities
{
    public class DriverLicense
    {
        public string LicenseNumber { get; set; }
        public string LicenseType { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
