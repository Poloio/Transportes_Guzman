using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class VehicleListViewComponent : ViewComponent
    {
        private readonly TransportContext _context;
        public VehicleListViewComponent(TransportContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync(string sortMode)
        {
            var viewModel = new VehicleListViewModel(sortMode, _context);
            viewModel.FillTable();
            return View(viewModel);
        }
    }
}
