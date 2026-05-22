using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Drivers;

public class EditModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Driver Driver { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var driver = await db.Drivers.FindAsync(id);
        if (driver is null)
            return NotFound();

        Driver = driver;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        db.Attach(Driver).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
