using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class RouteListViewComponent : ViewComponent
    {
        private readonly TransportContext _context;
        public RouteListViewComponent(TransportContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync(string sortMode)
        {
            var viewModel = new RouteListViewModel(sortMode, _context);
            viewModel.FillTable();
            return View(viewModel);
        }
    }
}
