using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Shipments;

public class IndexModel(LogisticsDbContext db) : PageModel
{
    public List<Shipment> Shipments { get; set; } = [];

    public async Task OnGetAsync()
    {
        Shipments = await db.Shipments
            .Include(s => s.Driver)
            .Include(s => s.Warehouse)
            .OrderByDescending(s => s.ShipDate)
            .ToListAsync();
    }
}
