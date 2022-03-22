using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransGuzman_Entities.Models
{
    public class Transporter
    {
        //PK,  don't modify
        [Display(Name = "ID de Empleado")]
        public string TransporterID { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "DNI/Permiso")]
        public string DriverLicenseID { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = @"Año de nacimiento")]
        public int YearOfBirth { get; set; }

        public ICollection<Route> Routes;
        public DriverLicense DriverLicense { get; set; }
    }
}
