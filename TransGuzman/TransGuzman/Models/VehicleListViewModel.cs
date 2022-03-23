using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransGuzman_Entities;
using TransGuzman_UI.Models.Interfaces;

namespace TransGuzman_UI.Models
{
    public class VehicleListViewModel : IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }

        private readonly TransportContext _context;

        public VehicleListViewModel(String _sortMode, TransportContext context)
        {
            _context = context;
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

        public void FillTable()
        {
            var table = CreateDataTable();
            var tempTableView = table.DefaultView;
            tempTableView = OrderTable(tempTableView, SortMode);
            EntitiesTable = tempTableView.ToTable();
        }

        private DataTable CreateDataTable()
        {
            DataTable viewTable = new DataTable();

            //Construct a view table for the datatable using LINQ query
            var viewRowsList = (from vehicle in _context.Vehicles
                                select new
                                {
                                    VehicleID = vehicle.VehicleID,
                                    ModelName = vehicle.ModelName,
                                    RequiredLicense = vehicle.RequiredDriverLicenseID,
                                    MaxCargo = vehicle.MaxCargo,
                                    TotalKm = _context.Routes.Where(rt => rt.VehicleID == vehicle.VehicleID).Sum(km => km.TraveledKM)
                                }).ToList();

            AddColumnsToDataTable(viewTable);

            //Fill datatable with rows
            viewRowsList.ForEach(row =>
            {
                viewTable.Rows.Add(row);
            });

            return viewTable;
        }

        private void AddColumnsToDataTable(DataTable viewTable)
        {
            viewTable.Columns.Add(new DataColumn { ColumnName = "Matrícula" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Modelo" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Licencia requerida" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Carga máxima" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Kilómetros" });
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
