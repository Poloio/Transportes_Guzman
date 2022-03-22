using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_DAL;

namespace TransGuzman_BL
{
    public static class VehiclesBL
    {
        public static async Task<DataTable> GetDataTableAsyncBL()
        {
            return await VehiclesDAL.GetDataTableAsyncDAL();
        }
    }
}
