using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;

namespace TransGuzman_UI.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly TransportContext _context;
        public VehiculosController(TransportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
