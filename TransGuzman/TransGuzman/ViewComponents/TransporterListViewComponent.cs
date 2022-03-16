using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;
using TransGuzman_Entities;

namespace TransGuzman_UI.ViewComponents
{
    public class TransporterListViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var transporters = await GetItemsAsync();
            return View(transporters);
        }


        private async Task<List<Transporter>> GetItemsAsync()
        {
            return await TransportersBL.GetAllTransportersAsyncBL();
        }
    }
}
