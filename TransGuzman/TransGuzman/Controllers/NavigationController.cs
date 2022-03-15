using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransGuzman_UI.Controllers
{
    public class NavigationController : Controller
    {
        public IActionResult Menu()
        {
            return PartialView("_Sidebar");
        }
    }
}
