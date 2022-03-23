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
    public class RouteListViewModel : IListViewmodel
    {
        public List<SelectListItem> OrderSelectItems { get; set; }
        public SelectListItem SelectedListItem { get; set; }
        public DataTable EntitiesTable { get; set; }
        public string SortMode { get; set; }
        private readonly TransportContext _context;

        public RouteListViewModel(string _sortmode, TransportContext context)
        {
            _context = context;
            SortMode = _sortmode;
            FillSelectItems();
            SetSelectedItem();
            EntitiesTable = CreateDataTable();
            var tempTableView = EntitiesTable.DefaultView;
            tempTableView = OrderTable(tempTableView);
            EntitiesTable = tempTableView.ToTable();
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

        public void FillTable()
        {
            var table = CreateDataTable();
            var tempTableView = table.DefaultView;
            tempTableView = OrderTable(tempTableView);
            EntitiesTable = tempTableView.ToTable();
        }

        private DataTable CreateDataTable()
        {
            DataTable viewTable = new DataTable();

            //Construct a view table for the datatable using LINQ query
            var viewRowsList = (from route in _context.Routes
                                select new
                                {
                                    ID_Ruta = route.RouteID,
                                    DNI_Conductor = _context.Transporters.First(tr => tr.TransporterID == route.TransporterID)
                                       .DriverLicenseID,
                                    Matricula_de_Vehiculo = route.VehicleID,
                                    Provincia_de_origen = _context.Provinces.First(prv => prv.ID == route.OriginProvinceID)
                                       .Name,
                                    Provincia_de_destino = _context.Provinces.First(prv => prv.ID == route.DestinationProvinceID)
                                       .Name,
                                    Distancia_en_KM = route.TraveledKM,
                                    Carga = _context.RoutePackages.Join(
                                               _context.Packages,
                                               rtPackage => rtPackage.PackageID,
                                               package => package.PackageID,
                                               (rtPackage, package) => new
                                               {
                                                   RouteID = rtPackage.RouteID,
                                                   PackageID = package.PackageID,
                                                   Weight = package.Weight
                                               }
                                           ).Where(rec => rec.RouteID == route.RouteID)
                                           .Select(rec => rec.Weight ?? 0).Sum()
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
            viewTable.Columns.Add(new DataColumn { ColumnName = "ID Ruta" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "DNI de conductor" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Matrícula de vehículo" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Origen" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Destino" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Distancia (KM)" });
            viewTable.Columns.Add(new DataColumn { ColumnName = "Carga (Kg)" });
        }

    }
}
