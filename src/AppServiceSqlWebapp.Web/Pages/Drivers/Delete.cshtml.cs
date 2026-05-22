using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Drivers;

public class DeleteModel(LogisticsDbContext db) : PageModel
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
        var driver = await db.Drivers.FindAsync(Driver.Id);
        if (driver is null)
            return NotFound();

        db.Drivers.Remove(driver);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
