using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities
{
    public class LicenseType
    {
        public string Name { get; set; }

        public ICollection<Transporter> Transporters { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
