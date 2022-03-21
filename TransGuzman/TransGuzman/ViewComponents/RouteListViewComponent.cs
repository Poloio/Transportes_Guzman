﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_UI.Models;

namespace TransGuzman_UI.ViewComponents
{
    public class RouteListViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string sortMode)
        {
            var viewModel = new RouteListViewModel(sortMode);
            await viewModel.FillTableAsync();
            return View(viewModel);
        }
    }
}
