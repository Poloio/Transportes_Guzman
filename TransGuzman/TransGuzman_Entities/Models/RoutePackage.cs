using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class RoutePackage
    {
        public int PackageID { get; set; }
        public int RouteID { get; set; }
        public Package Package { get; set; }
        public Route Route { get; set; }
    }
}
