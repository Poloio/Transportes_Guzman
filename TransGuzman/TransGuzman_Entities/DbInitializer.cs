using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransGuzman_Entities.Models;

namespace TransGuzman_Entities
{
    public static class DbInitializer
    {
        public static void Initialize(TransportContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Transporters.Any())
            {
                return;   // DB has been seeded
            }

            var transporters = new Transporter[]
            {
                new Transporter {TransporterID=new Guid(),}
            }
        }
}
