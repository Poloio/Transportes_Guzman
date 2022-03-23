using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_Entities.Models;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterListViewComponent : ViewComponent
    {
        private readonly TransportContext _context;
        public TransporterListViewComponent(TransportContext context)
        {
            _context = context;
        }

        public IViewComponentResult InvokeAsync(string sortMode)
        {
            var viewModel = new TransporterListViewModel(sortMode, _context);
            viewModel.FillTable();
            return View(viewModel);
        }
    }
}
