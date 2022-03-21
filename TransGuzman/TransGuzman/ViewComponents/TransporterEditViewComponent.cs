using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;
using TransGuzman_Entities;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterEditViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Transporter transporter)
        {
            return View(transporter);
        }
    }
}
