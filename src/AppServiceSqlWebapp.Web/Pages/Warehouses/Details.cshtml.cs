using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Warehouses;

public class DetailsModel(LogisticsDbContext db) : PageModel
{
    public Warehouse? Warehouse { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Warehouse = await db.Warehouses.FindAsync(id);
        if (Warehouse is null)
            return NotFound();

        return Page();
    }
}
