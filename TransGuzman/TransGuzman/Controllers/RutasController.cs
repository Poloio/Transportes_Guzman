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
        public async Task<IActionResult> Crear(CreateRouteViewModel routeVM)
        {

            IActionResult result;
            if (routeVM != null)
            {
                try
                {
                    var driverID = await TransportersBL.GetTransporterIDByLicenseAsyncBL(routeVM.DriverLicenseNumber);
                    var newRoute = new Route();
                    newRoute.DriverID = driverID;
                    newRoute.TruckLicenseNumber = routeVM.VehicleLicensePlate;
                    newRoute.OriginCode = routeVM.OriginProvince;
                    newRoute.DestinationCode = routeVM.DestinationProvince;
                    newRoute.Kilometers = routeVM.Distance;

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
