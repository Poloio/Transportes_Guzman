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
    public class TransportistasController : Controller
    {
        private readonly TransportContext _context;

        public TransportistasController(TransportContext context)
        {
            _context = context;
        }

        public IActionResult Index(string sortMode)
        {
            return View((object) sortMode);
        }

        
        public async Task<IActionResult> Detalles(int? id)
        {
            IActionResult result;
            if (id == null) 
            {
                result = RedirectToAction("Index");
            } else
            {
                try
                {
                    var transporter = await _context.Transporters.FindAsync(id);
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
        public async Task<IActionResult> Crear(TransporterCreateViewModel viewmodel)
        {
            //DataAnnotations TODO

            IActionResult result;
            if (viewmodel != null)
            { 
                try
                {
                    var licenseAddResult = await _context.DriverLicenses.AddAsync(viewmodel.License);
                    var transporterAddResult = await _context.Transporters.AddAsync(viewmodel.Transporter);
                    bool succeeded = transporterAddResult.State == EntityState.Added
                        && licenseAddResult.State == EntityState.Added;
                    if (succeeded)
                    {
                        await _context.SaveChangesAsync();
                        result = View("Index");
                    }
                    else
                    {
                        _context.ChangeTracker.Entries()
                            .Where(e => e.Entity != null).ToList()
                            .ForEach(e => e.State = EntityState.Detached);
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

        public async Task<IActionResult> Editar(int? id)
        {
            IActionResult result;
            if (id == null)
            {
                result = RedirectToAction("Error","Home");
            }
            else
            {
                try
                {
                    var transporter = await _context.Transporters.FindAsync(id);
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
            var EmployeeID = newTransporter.TransporterID;
            var IDLicense = newTransporter.DriverLicenseID;
            var FirstName = newTransporter.FirstName;
            var LastName = newTransporter.LastName;
            var YearOfBirth = newTransporter.YearOfBirth;

            IActionResult result;
            if (ModelState.IsValid)
            {
                var updatedTransporter = new Transporter();
                updatedTransporter.TransporterID = EmployeeID;
                updatedTransporter.DriverLicenseID = IDLicense;
                updatedTransporter.FirstName = FirstName;
                updatedTransporter.LastName = LastName;
                updatedTransporter.YearOfBirth = YearOfBirth;

                bool succeeded = _context.Update(updatedTransporter).State == EntityState.Modified;
                if (succeeded)
                {
                    await _context.SaveChangesAsync();
                    result = RedirectToAction("Index");
                }
                else
                    _context.ChangeTracker.Entries()
                            .Where(e => e.Entity != null).ToList()
                            .ForEach(e => e.State = EntityState.Detached);
                    result = RedirectToAction("Error", "Home");
            }
            else
            {
                result = View(newTransporter);
            }
            return result;
        }

        public async Task<IActionResult> Borrar(int id)
        {
            IActionResult result;
            var transporterToDelete = _context.Transporters.Find(id);
            
            var successful = _context.Transporters.Remove(transporterToDelete).State == EntityState.Deleted;
            if (successful)
            {
                await _context.SaveChangesAsync();
                result = RedirectToAction("Index");
            }
            else
            {
                _context.ChangeTracker.Entries()
                            .Where(e => e.Entity != null).ToList()
                            .ForEach(e => e.State = EntityState.Detached);
                result = RedirectToAction("Error", "Home");
            }
            return result;
        }
    }
}
