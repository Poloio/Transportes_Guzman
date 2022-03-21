using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_UI.Models
{
    public class TransporterWithLicenseViewModel
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "DNI/Permiso")]
        public string IDLicense { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Display(Name = @"Año de nacimiento")]
        public int YearOfBirth { get; set; }

        [Required(ErrorMessage = "Se necesita un tipo de licencia")]
        [Display(Name = "Tipo de Licencia")]
        public string LicenseType { get; set; }

        [Display(Name = "Fecha de caducidad")]
        public DateTime ExpireDate { get; set; }

        public Transporter Transporter
        {
            get
            {
                return new Transporter(IDLicense, FirstName, LastName, YearOfBirth);
            }
        }

        public DriverLicense License
        {
            get
            {
                return new DriverLicense(IDLicense, LicenseType, ExpireDate);
            }
        }
    }

    
}
