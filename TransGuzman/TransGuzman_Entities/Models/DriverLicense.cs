using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class DriverLicense
    {
        public string LicenseID { get; set; }
        public string LicenseTypeID { get; set; }
        public DateTime ExpireDate { get; set; }

        public Transporter Transporter { get; set; }
        public LicenseType LicenseType { get; set; }
    }
}
