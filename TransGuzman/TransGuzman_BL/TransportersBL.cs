using System;
using System.Collections.Generic;
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
            return await TransportersDAL.GetAllTransportersAsyncDAL();
        }
    }
}
