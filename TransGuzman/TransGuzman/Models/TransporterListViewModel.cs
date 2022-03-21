using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_BL;

namespace TransGuzman_UI.Models
{
    public class TransporterListViewModel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable TransportersTable { get; set; }
        public string SortMode;
        
        public TransporterListViewModel (string sortMode)
        {
            this.SortMode = sortMode;
            //Fill select items
            FillSelectItems();
            SetSelectedItem(this.SortMode);
        }

        private void FillSelectItems()
        {
            OrderSelectItems = new List<SelectListItem>();
            OrderSelectItems.Add(new SelectListItem { Text = "Ordenar por...", Value = "" });
            OrderSelectItems.Add(new SelectListItem { Text = "Nombre", Value = "nombre" });
            OrderSelectItems.Add(new SelectListItem { Text = "Apellidos", Value = "apellidos" });
            OrderSelectItems.Add(new SelectListItem { Text = "Año de Nacimiento", Value = "anio_nacimiento" });
        }

        private void SetSelectedItem(string sortMode)
        {
            if (String.IsNullOrEmpty(sortMode))
            {
                OrderSelectItems[0].Selected = true;
                return; //No sé que te parece termnar con un return para evitar demasiada indentación
            }

            foreach (var item in OrderSelectItems)
            {
                if (item.Value.Equals(sortMode))
                    item.Selected = true;
                else
                    item.Selected = false;
            }
        }

        public async Task FillTable()
        {
            TransportersTable = await GetItemsAsync();
            var tempTableView = TransportersTable.DefaultView;
            tempTableView = OrderTable(tempTableView, SortMode);
            TransportersTable = tempTableView.ToTable();
        }

        private DataView OrderTable(DataView tempTableView, string sortMode)
        {
            switch (sortMode)
            {
                case "nombre":
                    tempTableView.Sort = "nombre";
                    break;
                case "apellidos":
                    tempTableView.Sort = "apellidos";
                    break;
                case "anio_nacimiento":
                    tempTableView.Sort = "anio_nacimiento";
                    break;
                default:
                    tempTableView.Sort = "nombre";
                    break;
            }

            return tempTableView;
        }

        private async Task<DataTable> GetItemsAsync()
        {
            return await TransportersBL.GetTransportersDataTableAsync();
        }

    }
}
