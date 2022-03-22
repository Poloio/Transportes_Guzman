using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities
{
    public class DriverLicense
    {
        public string LicenseID { get; set; }
        public string LicenseType { get; set; }
        public DateTime ExpireDate { get; set; }

        public ICollection<Transporter> Transporters { get; set; }
         
        public DriverLicense(string licenseNumber, string licenseType, DateTime expireDate)
        {
            LicenseID = licenseNumber;
            LicenseType = licenseType;
            ExpireDate = expireDate;
        }
    }
}
