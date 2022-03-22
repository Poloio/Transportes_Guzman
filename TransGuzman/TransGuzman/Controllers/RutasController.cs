using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.Controllers
{
    public class RutasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(RouteCreateViewModel routeVM)
        {

            IActionResult result;
            if (routeVM != null)
            {
                try
                {
                    var driverID = await TransportersBL.GetTransporterIDByLicenseAsyncBL(routeVM.DriverLicenseNumber);
                    var newRoute = new Route();
                    newRoute.TransporterID = driverID;
                    newRoute.VehicleID = routeVM.VehicleLicensePlate;
                    newRoute.OriginProvinceID = routeVM.OriginProvince;
                    newRoute.DestinatinProvinceID = routeVM.DestinationProvince;
                    newRoute.TraveledKM = routeVM.Distance;

                    bool succeeded = await RoutesBL.CreateNewAsyncBL(newRoute);
                    if (succeeded)
                    {
                        result = View("Index");
                    }
                    else
                    {
                        result = RedirectToAction("Error", "Home");
                    }
                }
                catch (Exception e)
                {
                    result = RedirectToAction("Error", "Home");
                }
            }
            else
            {
                result = RedirectToAction("Error", "Home");
            }
            return result;
        }
    }
}
