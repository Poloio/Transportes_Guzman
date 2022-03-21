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
    public class TransportistasController : Controller
    {
        public IActionResult Index(string sortMode)
        {
            return View((object) sortMode);
        }

        
        public async Task<IActionResult> Detalles(string employeeId)
        {
            IActionResult result;
            if (employeeId == null) 
            {
                result = RedirectToAction("Index");
            } else
            {
                try
                {
                    var transporter = await TransportersBL.GetTransporterAsyncBL(employeeId);
                    if (transporter != null)
                        result = View(transporter);
                    else //IF transporter doesn't exist
                        result = RedirectToAction("Error", "Home");
                } catch (Exception e)
                {
                    result = RedirectToAction("Error", "Home");
                }
            }// END Null check
            return result;
        }

        public  IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TransporterWithLicenseViewModel viewmodel)
        {
            //DataAnnotations TODO

            IActionResult result;
            if (viewmodel != null)
            { 
                try
                {
                    bool licenseOk = await DriverLicensesBL.CreateNewAsyncBL(viewmodel.License);
                    bool transporterOk = await TransportersBL.CreateNewAsyncBL(viewmodel.Transporter);
                    bool succeeded = licenseOk && transporterOk;
                    if (succeeded)
                    {
                        result = View("Index");
                    }
                    else
                    {
                        result = RedirectToAction("Error", "Home");
                    }
                } catch (Exception e)
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

        public async Task<IActionResult> Editar(string employeeId)
        {
            IActionResult result;
            if (employeeId == null)
            {
                result = RedirectToAction("Error","Home");
            }
            else
            {
                try
                {
                    var transporter = await TransportersBL.GetTransporterAsyncBL(employeeId);
                    result = View(transporter);
                } catch (Exception e)
                {
                    result = RedirectToAction("Error", "Home");
                }
            }
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Transporter newTransporter)
        {
            var EmployeeID = newTransporter.EmployeeID;
            var IDLicense = newTransporter.IDLicense;
            var FirstName = newTransporter.FirstName;
            var LastName = newTransporter.LastName;
            var YearOfBirth = newTransporter.YearOfBirth;

            IActionResult result;
            if (ModelState.IsValid)
            {
                var updatedTransporter = new Transporter();
                updatedTransporter.EmployeeID = EmployeeID;
                updatedTransporter.IDLicense = IDLicense;
                updatedTransporter.FirstName = FirstName;
                updatedTransporter.LastName = LastName;
                updatedTransporter.YearOfBirth = YearOfBirth;

                bool succeeded = await TransportersBL.UpdateTransporterAsyncBL(updatedTransporter);
                if (succeeded)
                    result = RedirectToAction("Index");
                else
                    result = RedirectToAction("Error", "Home");
            }
            else
            {
                result = View(newTransporter);
            }
            return result;
        }

        public async Task<IActionResult> Borrar(string employeeId)
        {
            IActionResult result;
            bool deleteSuccessful = await TransportersBL.DeleteByIDAsyncBL(employeeId);
            if (deleteSuccessful)
            {
                result = RedirectToAction("Index");
            }
            else
            {
                result = RedirectToAction("Error", "Home");
            }
            return result;
        }
    }
}
