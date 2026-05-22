using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Warehouses;

public class CreateModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Warehouse Warehouse { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        db.Warehouses.Add(Warehouse);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
