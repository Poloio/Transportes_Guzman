using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public int ClientID { get; set; }
        public int? Weight { get; set; }

        public Client Client { get; set; }
        public ICollection<RoutePackage> RoutePackages { get; set; }
    }
}
