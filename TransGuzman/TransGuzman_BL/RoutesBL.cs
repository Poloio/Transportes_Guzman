using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_DAL;
using TransGuzman_Entities;

namespace TransGuzman_BL
{
    public static class RoutesBL
    {
        /// <summary>
        /// Calls the DAL and retrieves all routes from it.
        /// </summary>
        /// <returns>a list of <see cref="Route"/>.</returns>
        public static async Task<List<Route>> GetAllAsyncBL()
        {
            return await RoutesDAL.GetAllAsyncDAL();
        }

        /// <summary>
        /// Calls the DAL and retrieves all route data in the form of a <see cref="DataTable"/>
        /// </summary>
        /// <returns>a <see cref="DataTable"/>.</returns>
        public static async Task<DataTable> GetDataTableAsync()
        {
            return await RoutesDAL.GetDataTableAsync();
        }

        /// <summary>
        /// Gets a single transporter from the DAL, the one with the same <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Route"/> object.</returns>
        public static async Task<Route> GetTransporterAsyncBL(int id)
        {
            return await RoutesDAL.GetAsyncDAL(id);
        }

        /// <summary>
        /// Calls the DAL to create a new Route in the Database.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>true if the route was successfully created.</returns>
        public static async Task<bool> CreateNewAsyncBL(Route route)
        {
            return await RoutesDAL.CreateNewAsyncDAL(route);
        }

        /// <summary>
        /// Calls the DAL to update a route passed through parameter.
        /// </summary>
        /// <param name="route"></param>
        /// <returns>true if anything was changed.</returns>
        public static async Task<bool> UpdateAsyncBL(Route route)
        {
            return await RoutesDAL.UpdateAsync(route);
        }

        /// <summary>
        /// Calls the DAL to delete a route in the db with a given route number.
        /// </summary>
        /// <param name="routeNumber"></param>
        /// <returns>true if anything was deleted.</returns>
        public static async Task<bool> DeleteByIDAsyncBL(int routeNumber)
        {
            return await RoutesDAL.DeleteByIDAsyncDAL(routeNumber);
        }
    }
}
