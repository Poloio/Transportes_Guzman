using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TransGuzman_UI.Models.Interfaces
{
    public interface IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }

        public Task FillTableAsync();
    }
}
