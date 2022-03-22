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
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new RouteCreateViewModel();
            await vm.FillProvinceOptions();
            return View(vm);
        }
    }
}
