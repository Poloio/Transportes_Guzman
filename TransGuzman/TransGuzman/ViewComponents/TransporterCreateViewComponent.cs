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
        private readonly TransportContext _context;

        public TransporterCreateViewComponent(TransportContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync(string sortMode)
        {
            var viewmodel = new TransporterCreateViewModel(_context);
            viewmodel.FillLicenseTypeOptions();
            return View(viewmodel);
        }
    }
}
