using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Province
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Route> OriginRoutes { get; set; }
        public ICollection<Route> DestinationRoutes { get; set; }

    }
}
