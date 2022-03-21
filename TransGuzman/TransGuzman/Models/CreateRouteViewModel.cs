using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_DAL;

namespace TransGuzman_UI.Models
{
    public class CreateRouteViewModel
    {
        [Display(Name = "DNI de conductor")]
        public string DriverLicenseNumber { get; set; }

        [Display(Name = "Matrícula de vehículo")]
        public string VehicleLicensePlate { get; set; }

        [Display(Name = "Provincia de origen*")]
        [Required(ErrorMessage = "Campo requerido")]
        public int OriginProvince { get; set; }

        [Display(Name = "Provincia de destino*")]
        [Required(ErrorMessage = "Campo requerido")]
        public int DestinationProvince { get; set; }

        [Display(Name = "Kilómetros recorridos")]
        public int Distance { get; set; }

        public List<SelectListItem> ProvinceOptions { get; set; }

        public async Task FillProvinceOptions()
        {
            ProvinceOptions = new List<SelectListItem>();
            foreach (var province in await ProvincesDAL.GetAllAsync())
            {
                ProvinceOptions.Add(new SelectListItem { Text = province.Name, Value = province.ID.ToString() });
            }
        }
    }
}
