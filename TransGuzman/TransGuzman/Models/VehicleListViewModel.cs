using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;
using TransGuzman_UI.Models.Interfaces;

namespace TransGuzman_UI.Models
{
    public class VehicleListViewModel : IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }

        public VehicleListViewModel(String _sortMode)
        {
            SortMode = _sortMode;
            FillSelectItems();
            SetSelectedItem();
        }

        private void FillSelectItems()
        {
            OrderSelectItems = new List<SelectListItem>();
            OrderSelectItems.Add(new SelectListItem { Text = "Ordenar por...", Value = "" });
            OrderSelectItems.Add(new SelectListItem { Text = "Modelo", Value = "modelo" });
            OrderSelectItems.Add(new SelectListItem { Text = "Matrícula", Value = "matricula" });
        }

        private void SetSelectedItem()
        {
            if (String.IsNullOrEmpty(SortMode))
            {
                OrderSelectItems[0].Selected = true;
                return; //No sé que te parece termnar con un return para evitar demasiada indentación
            }

            foreach (var item in OrderSelectItems)
            {
                if (item.Value.Equals(SortMode))
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }

        public async Task FillTableAsync()
        {
            var table = await VehiclesBL.GetDataTableAsyncBL();
            var tempTableView = table.DefaultView;
            tempTableView = OrderTable(tempTableView, SortMode);
            EntitiesTable = tempTableView.ToTable();
        }

        private DataView OrderTable(DataView tempTableView, string sortMode)
        {
            switch (sortMode)
            {
                case "modelo":
                    tempTableView.Sort = "modelo";
                    break;
                case "matricula":
                    tempTableView.Sort = "matricula";
                    break;
                default:
                    tempTableView.Sort = "modelo";
                    break;
            }
            return tempTableView;
        }
    }
}
