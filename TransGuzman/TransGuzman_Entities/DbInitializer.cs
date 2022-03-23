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
            if (context.Transporters.Any())
            {
                return;   // DB has been seeded
            }

            var licenseTypes = new LicenseType[]
            {
                new LicenseType { Name = "A" },
                new LicenseType { Name = "A1" },
                new LicenseType { Name = "A2" },
                new LicenseType { Name = "B" },
                new LicenseType { Name = "B+E" },
                new LicenseType { Name = "BTP" },
                new LicenseType { Name = "C" },
                new LicenseType { Name = "C+E" },
                new LicenseType { Name = "C1" },
                new LicenseType { Name = "C1+E" },
                new LicenseType { Name = "D" },
                new LicenseType { Name = "D1" },
                new LicenseType { Name = "D1+E" }
            };
            foreach (var type in licenseTypes)
            {
                context.LicenseTypes.Add(type);
            }
            context.SaveChanges();

            var driverLicenses = new DriverLicense[]
            {
                new DriverLicense {LicenseID = "49106316M", LicenseTypeID = "B", ExpireDate = DateTime.Parse("10-10-2022")},
                new DriverLicense {LicenseID = "23432432J", LicenseTypeID = "C", ExpireDate = DateTime.Parse("08-08-2026")},
                new DriverLicense {LicenseID = "49106317Y", LicenseTypeID = "A", ExpireDate = DateTime.Parse("22-04-2023")},
                new DriverLicense {LicenseID = "80085717S", LicenseTypeID = "B+E", ExpireDate = DateTime.Parse("06-01-2026")}
            };
            foreach (var license in driverLicenses)
            {
                context.DriverLicenses.Add(license);
            }
            context.SaveChanges();

            var transporters = new Transporter[]
            {
                new Transporter {DriverLicenseID = "49106316M", FirstName = "Carlos", LastName = "Aragón Pelayo", YearOfBirth = 1999},
                new Transporter {DriverLicenseID = "23432432J", FirstName = "Paco", LastName = "Toronjo", YearOfBirth = 1965},
                new Transporter {DriverLicenseID = "49106317Y", FirstName = "Victoriano", LastName = "Aragón Pelayo", YearOfBirth = 2001},
                new Transporter {DriverLicenseID = "80085717S", FirstName = "Pablo", LastName = "Calvo", YearOfBirth = 2000}
            };
            foreach (var transporter in transporters)
            {
                context.Transporters.Add(transporter);
            }
            context.SaveChanges();

            var vehicles = new Vehicle[]
            {
                new Vehicle {VehicleID = "1287DKL", RequiredDriverLicenseID = "B", ModelName = "Cinca 1000", MaxCargo = 800},
                new Vehicle {VehicleID = "1902PTP", RequiredDriverLicenseID = "C", ModelName = "Iveco Transporter", MaxCargo = 3500},
                new Vehicle {VehicleID = "6488PRL", RequiredDriverLicenseID = "D", ModelName = "Mercedes FH-750", MaxCargo = 3900},
            };
            foreach(var vehicle in vehicles)
            {
                context.Vehicles.Add(vehicle);
            }
            context.SaveChanges();

            var provinces = new Province[]
            {
                new Province {ID = 1, Name = "Huelva"},
                new Province {ID = 2, Name = "Sevilla"},
                new Province {ID = 3, Name = "Cádiz"},
                new Province {ID = 4, Name = "Córdoba"},
                new Province {ID = 5, Name = "Málaga"},
                new Province {ID = 6, Name = "Jaén"},
                new Province {ID = 7, Name = "Granada"},
                new Province {ID = 8, Name = "Almería"}
            };
            foreach (var province in provinces)
            {
                context.Provinces.Add(province);
            }
            context.SaveChanges();

            var clients = new Client[]
            {
                new Client {Name = "Óptica Casimiro"},
                new Client {Name = "Detectives Truhán Hurtado"},
                new Client {Name = "Funeraria Paz"},
                new Client {Name = "Material militar Everis"},
                new Client {Name = "Pintura Maite Pinto"}
            };
            foreach (var client in clients)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();

            var packages = new Package[]
            {
                new Package {ClientID = 1, Weight = 500},
                new Package {ClientID = 2, Weight = 800},
                new Package {ClientID = 3, Weight = 14},
                new Package {ClientID = 4, Weight = 3000},
                new Package {ClientID = 5, Weight = 590}
            };
            foreach (var pckg in packages)
            {
                context.Packages.Add(pckg);
            }
            context.SaveChanges();

            var routes = new Route[]
            {
                new Route {TransporterID=1, VehicleID="1287DKL", OriginProvinceID=1, DestinationProvinceID=8, TraveledKM=763},
                new Route {TransporterID=1, VehicleID="6488PRL", OriginProvinceID=1, DestinationProvinceID=2, TraveledKM=149}
            };
            foreach (var route in routes)
            {
                context.Routes.Add(route);
            }
            context.SaveChanges();

            var routePackages = new RoutePackage[]
            {
                new RoutePackage {RouteID=1,PackageID=1},
                new RoutePackage {RouteID=1,PackageID=2},
                new RoutePackage {RouteID=1,PackageID=3},
                new RoutePackage {RouteID=2,PackageID=4},
                new RoutePackage {RouteID=2,PackageID=5}
            };
            foreach (var rtPckg in routePackages)
            {
                context.RoutePackages.Add(rtPckg);
            }
            context.SaveChanges();
        }
    }
}
