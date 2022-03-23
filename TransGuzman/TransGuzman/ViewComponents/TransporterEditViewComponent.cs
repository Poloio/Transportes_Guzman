using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_Entities.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterEditViewComponent : ViewComponent
    {
        private readonly TransportContext _context;
        public TransporterEditViewComponent(TransportContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync(Transporter transporter)
        {
            return View(transporter);
        }
    }
}
