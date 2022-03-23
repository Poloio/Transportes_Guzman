using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_Entities.Models;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.Controllers
{
    public class RutasController : Controller
    {
        private readonly TransportContext _context;

        public RutasController(TransportContext context)
        {
            _context = context;
        }

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
                    var transporter = _context.Transporters.First(tr => tr.DriverLicenseID == routeVM.DriverLicenseNumber);

                    var newRoute = new Route();
                    newRoute.TransporterID = transporter.TransporterID;
                    newRoute.VehicleID = routeVM.VehicleLicensePlate;
                    newRoute.OriginProvinceID = routeVM.OriginProvince;
                    newRoute.DestinationProvinceID = routeVM.DestinationProvince;
                    newRoute.TraveledKM = routeVM.Distance;

                    var addResult = await _context.Routes.AddAsync(newRoute);
                    var succeeded = addResult.State == EntityState.Added;
                    if (succeeded)
                    {
                        await _context.SaveChangesAsync();
                        result = View("Index");
                    }
                    else
                    {
                        addResult.State = EntityState.Detached;
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
