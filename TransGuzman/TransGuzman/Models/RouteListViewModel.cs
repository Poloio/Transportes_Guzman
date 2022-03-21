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
    public class RouteListViewModel : IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }

        public RouteListViewModel(string _sortmode)
        {
            SortMode = _sortmode;
            FillSelectItems();
            SetSelectedItem();
        }

        private void SetSelectedItem()
        {
            if (String.IsNullOrEmpty(SortMode))
            {
                OrderSelectItems[0].Selected = true;
                return; //No sé que te parece terminar con un return para evitar demasiada indentación
            }

            foreach (var item in OrderSelectItems)
            {
                if (item.Value.Equals(SortMode))
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }

        private void FillSelectItems()
        {
            OrderSelectItems = new List<SelectListItem>();
            OrderSelectItems.Add(new SelectListItem { Text = "Ordenar por...", Value = "" });
            OrderSelectItems.Add(new SelectListItem { Text = "Número (últimos antes)", Value = "numero" });
            OrderSelectItems.Add(new SelectListItem { Text = "Número (primeros antes)", Value = "numero-asc" });
            OrderSelectItems.Add(new SelectListItem { Text = "Distancia recorrida (menos a más)", Value = "km_recorridos" });
            OrderSelectItems.Add(new SelectListItem { Text = "Distancia recorrida(más a menos)", Value = "km_recorridos desc" });
        }


        public async Task FillTableAsync()
        {
            EntitiesTable = await GetItemsAsync();
            var tempTableView = EntitiesTable.DefaultView;
            tempTableView = OrderTable(tempTableView);
            EntitiesTable = tempTableView.ToTable();
        }

        private DataView OrderTable(DataView tempTableView)
        {
            switch (SortMode)
            {
                case "numero":
                    tempTableView.Sort = "numero_ruta desc";
                    break;
                case "numero-asc":
                    tempTableView.Sort = "numero ruta";
                    break;
                case "distancia":
                    tempTableView.Sort = "km_recorridos";
                    break;
                case "distancia-asc":
                    tempTableView.Sort = "km_recorridos";
                    break;
                default:
                    tempTableView.Sort = "numero_ruta desc";
                    break;
            }
            return tempTableView;
        }

        private async Task<DataTable> GetItemsAsync()
        {
            return await RoutesBL.GetDataTableAsync();
        }
    }
}
