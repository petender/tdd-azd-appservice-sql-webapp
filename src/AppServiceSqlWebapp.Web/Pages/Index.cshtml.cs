using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages;

public class IndexModel(LogisticsDbContext db) : PageModel
{
    public int TotalShipments { get; set; }
    public int TotalWarehouses { get; set; }
    public int TotalDrivers { get; set; }
    public Dictionary<string, int> ShipmentsByStatus { get; set; } = [];
    public Dictionary<string, int> DriversByStatus { get; set; } = [];
    public List<Shipment> RecentShipments { get; set; } = [];

    public async Task OnGetAsync()
    {
        TotalShipments = await db.Shipments.CountAsync();
        TotalWarehouses = await db.Warehouses.CountAsync();
        TotalDrivers = await db.Drivers.CountAsync();

        ShipmentsByStatus = await db.Shipments
            .GroupBy(s => s.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

        DriversByStatus = await db.Drivers
            .GroupBy(d => d.Status)
            .ToDictionaryAsync(g => g.Key, g => g.Count());

        RecentShipments = await db.Shipments
            .OrderByDescending(s => s.ShipDate)
            .Take(5)
            .ToListAsync();
    }
}
