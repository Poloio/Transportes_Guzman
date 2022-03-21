using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterCreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new TransporterWithLicenseViewModel());
        }
    }
}
