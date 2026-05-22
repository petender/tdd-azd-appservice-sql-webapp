using AppServiceSqlWebapp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Data;

public class LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : DbContext(options)
{
    public DbSet<Shipment> Shipments => Set<Shipment>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<Driver> Drivers => Set<Driver>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasIndex(e => e.TrackingNumber).IsUnique();
            entity.HasOne(e => e.Driver)
                  .WithMany(d => d.Shipments)
                  .HasForeignKey(e => e.DriverId)
                  .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Warehouse)
                  .WithMany(w => w.Shipments)
                  .HasForeignKey(e => e.WarehouseId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasIndex(e => e.LicenseNumber).IsUnique();
        });
    }
}
