using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_DAL;
using TransGuzman_Entities;

namespace TransGuzman_BL
{
    public static class TransportersBL
    {
        /// <summary>
        /// Calls the DAL and retrieves all transporters from it.
        /// </summary>
        /// <returns>a list of <see cref="Transporter"/>.</returns>
        public static async Task<List<Transporter>> GetAllTransportersAsyncBL()
        {
            return await TransportersDAL.GetAllAsyncDAL();
        }

        /// <summary>
        /// Calls the DAL and retrieves all transporters data in the form of a <see cref="DataTable"/>
        /// </summary>
        /// <returns>a <see cref="DataTable"/>.</returns>
        public static async Task<DataTable> GetTransportersDataTableAsync()
        {
            return await TransportersDAL.GetDataTableAsync();
        }
        
        /// <summary>
        /// Gets a single transporter from the DAL, the one with the same <paramref name="id"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a <see cref="Transporter"/> object.</returns>
        public static async Task<Transporter> GetTransporterAsyncBL(string id)
        {
            return await TransportersDAL.GetTransporterAsyncDAL(id);
        }

        public static async Task<bool> CreateNewAsyncBL(Transporter transporter)
        {
            return await TransportersDAL.CreateNewAsyncDAL(transporter);
        }

        public static async Task<bool> UpdateTransporterAsyncBL(Transporter transporter)
        {
            return await TransportersDAL.UpdateTransporterAsyncDAL(transporter);
        }

        public static async Task<bool> DeleteByIDAsyncBL(string employeeId)
        {
            return await TransportersDAL.DeleteByIDAsyncDAL(employeeId);
        }

        public static async Task<string> GetTransporterIDByLicenseAsyncBL(string licenseNumber)
        {
            return await TransportersDAL.GetTransporterIDByLicenseAsyncDAL(licenseNumber);
        }
    }
}
