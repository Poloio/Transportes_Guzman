using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class RouteCreateViewComponent : ViewComponent
    {
        private readonly TransportContext _context;
        public RouteCreateViewComponent(TransportContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var vm = new RouteCreateViewModel(_context);
            vm.FillProvinceOptions();
            return View(vm);
        }
    }
}
