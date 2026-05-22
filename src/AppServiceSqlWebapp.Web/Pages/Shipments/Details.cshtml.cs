using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Shipments;

public class DetailsModel(LogisticsDbContext db) : PageModel
{
    public Shipment? Shipment { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Shipment = await db.Shipments
            .Include(s => s.Driver)
            .Include(s => s.Warehouse)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (Shipment is null)
            return NotFound();

        return Page();
    }
}
