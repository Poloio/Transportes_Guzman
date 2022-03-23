using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransGuzman_Entities.Models;

namespace TransGuzman_Entities
{
    public class TransportContext : DbContext
    {
        public DbSet<Transporter> Transporters { get; set; }
        public DbSet<DriverLicense> DriverLicenses { get; set; }
        public DbSet<LicenseType> LicenseTypes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RoutePackage> RoutePackages { get; set; }

        public TransportContext(DbContextOptions<TransportContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here
            modelBuilder.Entity<Transporter>()
                    .Property(t => t.TransporterID)
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<Client>()
                    .Property(t => t.ClientID)
                    .ValueGeneratedOnAdd();


            modelBuilder.Entity<Package>()
                    .Property(t => t.PackageID)
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<Route>()
                    .Property(t => t.RouteID)
                    .ValueGeneratedOnAdd();

            modelBuilder.Entity<RoutePackage>()
                .HasKey(bc => new { bc.RouteID, bc.PackageID });
            modelBuilder.Entity<RoutePackage>()
                .HasOne(bc => bc.Route)
                .WithMany(b => b.RoutePackages)
                .HasForeignKey(bc => bc.RouteID);
            modelBuilder.Entity<RoutePackage>()
                .HasOne(bc => bc.Package)
                .WithMany(c => c.RoutePackages)
                .HasForeignKey(bc => bc.PackageID);
        }
    }
}
