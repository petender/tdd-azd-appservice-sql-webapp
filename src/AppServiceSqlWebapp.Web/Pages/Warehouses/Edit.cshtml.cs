using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Warehouses;

public class EditModel(LogisticsDbContext db) : PageModel
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
        if (!ModelState.IsValid)
            return Page();

        db.Attach(Warehouse).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
