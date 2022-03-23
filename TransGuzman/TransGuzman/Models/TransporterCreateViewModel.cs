using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_Entities.Models;

namespace TransGuzman_UI.Models
{
    public class TransporterCreateViewModel
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


        private readonly TransportContext _context;

        public TransporterCreateViewModel(TransportContext context)
        {
            _context = context;
        }

        public Transporter Transporter
        {
            get
            {
                return new Transporter { DriverLicenseID = IDLicense, FirstName = FirstName, LastName = LastName, YearOfBirth = YearOfBirth};
            }
        }

        public DriverLicense License
        {
            get
            {
                return new DriverLicense { LicenseID = IDLicense, LicenseTypeID = LicenseType, ExpireDate = ExpireDate };
            }
        }

        public List<SelectListItem> LicenseTypeOptions { get; set; }

        public void FillLicenseTypeOptions()
        {
            LicenseTypeOptions = new List<SelectListItem>();
            var typeList = _context.LicenseTypes.ToList();
            foreach (var type in typeList)
            {
                LicenseTypeOptions.Add(new SelectListItem { Text = type.Name, Value = type.Name });
            }
        }
    }

    
}
