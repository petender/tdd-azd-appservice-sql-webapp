using System.Text.Json;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(LogisticsDbContext context, IWebHostEnvironment env)
    {
        await context.Database.MigrateAsync();

        if (await context.Drivers.AnyAsync())
            return;

        var seedPath = Path.Combine(env.ContentRootPath, "SeedData");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var driversJson = await File.ReadAllTextAsync(Path.Combine(seedPath, "drivers.json"));
        var drivers = JsonSerializer.Deserialize<List<Driver>>(driversJson, options) ?? [];
        context.Drivers.AddRange(drivers);

        var warehousesJson = await File.ReadAllTextAsync(Path.Combine(seedPath, "warehouses.json"));
        var warehouses = JsonSerializer.Deserialize<List<Warehouse>>(warehousesJson, options) ?? [];
        context.Warehouses.AddRange(warehouses);

        await context.SaveChangesAsync();

        var shipmentsJson = await File.ReadAllTextAsync(Path.Combine(seedPath, "shipments.json"));
        var shipments = JsonSerializer.Deserialize<List<Shipment>>(shipmentsJson, options) ?? [];
        context.Shipments.AddRange(shipments);

        await context.SaveChangesAsync();
    }
}
