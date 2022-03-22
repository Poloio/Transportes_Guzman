using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_DAL;
using TransGuzman_Entities;

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

        public List<SelectListItem> LicenseTypeOptions { get; set; }

        public async Task FillLicenseTypeOptions()
        {
            LicenseTypeOptions = new List<SelectListItem>();
            var typeList = await DriverLicensesDAL.GetLicenseTypesAsync();
            foreach (var type in typeList)
            {
                LicenseTypeOptions.Add(new SelectListItem { Text = type.Name, Value = type.Name });
            }
        }
    }

    
}
