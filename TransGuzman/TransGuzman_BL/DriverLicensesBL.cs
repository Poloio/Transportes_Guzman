using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransGuzman_DAL;
using TransGuzman_Entities;

namespace TransGuzman_BL
{
    public static class DriverLicensesBL
    {
        public static async Task<bool> CreateNewAsyncBL(DriverLicense license)
        {
            return await DriverLicensesDAL.CreateNewAsyncDAL(license);
        }
        
    }
}
