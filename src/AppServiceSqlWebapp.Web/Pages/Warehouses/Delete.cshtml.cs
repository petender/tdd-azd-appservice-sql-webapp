using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Warehouses;

public class DeleteModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Warehouse Warehouse { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var warehouse = await db.Warehouses.FindAsync(id);
        if (warehouse is null)
            return NotFound();

        Warehouse = warehouse;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var warehouse = await db.Warehouses.FindAsync(Warehouse.Id);
        if (warehouse is null)
            return NotFound();

        db.Warehouses.Remove(warehouse);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
