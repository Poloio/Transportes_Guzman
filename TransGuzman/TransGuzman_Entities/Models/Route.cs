using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Route
    {
        [Display(Name = "Número de ruta")]
        public int RouteID { get; set; }

        [Display(Name = "DNI Conductor")]
        public string? TransporterID { get; set; }

        [Display(Name = "Matrícula de vehículo")]
        public string? VehicleID { get; set; }

        [Display(Name = "Provincia de origen")]
        [Required(ErrorMessage = "Campo requerido")]
        public int OriginProvinceID { get; set; }

        [Display(Name = "Provincia de destino")]
        [Required(ErrorMessage = "Campo requerido")]
        public int DestinatinProvinceID { get; set; }

        [Display(Name = "Distancia recorrida")]
        public int? TraveledKM { get; set; }

        public Transporter Transporter { get; set; }
        public Vehicle Vehicle { get; set; }
        public Province OriginProvince { get; set; }
        public Province DestinationProvince { get; set; }

        public ICollection<RoutePackage> RoutePackages { get; set; }
    }
}
