using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransGuzman_Entities
{
    public class Route
    {
        [Display(Name = "Número de ruta")]
        public int Number { get; set; }
        
        [Display(Name = "DNI Conductor")]
        public string DriverID { get; set; }

        [Display(Name = "Matrícula de vehículo")]
        public string TruckLicenseNumber { get; set; }

        [Display(Name = "Provincia de origen")]
        [Required(ErrorMessage = "Campo requerido")]
        public int OriginCode { get; set; }

        [Display(Name = "Provincia de destino")]
        [Required(ErrorMessage = "Campo requerido")]
        public int DestinationCode { get; set; }

        [Display(Name = "Distancia recorrida")]
        public int Kilometers { get; set; }
    }
}
