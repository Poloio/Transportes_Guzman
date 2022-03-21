using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;
using TransGuzman_Entities;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterListViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(string sortMode)
        {
            var viewModel = new TransporterListViewModel(sortMode);
            await viewModel.FillTable();
            return View(viewModel);
        }
    }
}
