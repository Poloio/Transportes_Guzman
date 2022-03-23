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
    public class TransporterListViewModel : IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }

        private readonly TransportContext _context;
        
        public TransporterListViewModel (string sortMode, TransportContext context)
        {
            _context = context;
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

        public void FillTable()
        {
            var table = CreateDataTable();
            var tempTableView = EntitiesTable.DefaultView;
            tempTableView = OrderTable(tempTableView, SortMode);
            EntitiesTable = tempTableView.ToTable();
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

        private DataTable CreateDataTable()
        {
            DataTable viewTable = new DataTable();

            //Construct a view table for the datatable using LINQ query
            var viewRowsList = (from tp in _context.Transporters
                                select new
                                {
                                    LicenseID = tp.DriverLicenseID,
                                    LicenseType = _context.DriverLicenses.First(dl => dl.LicenseID == tp.DriverLicenseID)
                                        .LicenseTypeID,
                                    FirstName = tp.FirstName,
                                    LastName = tp.LastName,
                                    YearOfBirth = tp.YearOfBirth
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
            viewTable.Columns.Add(new DataColumn { ColumnName = "DNI" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Tipo de permiso" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Nombre" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Apellidos" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Año de nacimeinto" });
        }
    }
}
