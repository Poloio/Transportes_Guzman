using System;
using System.Collections.Generic;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public ICollection<Package> Packages { get; set; }
    }
}
