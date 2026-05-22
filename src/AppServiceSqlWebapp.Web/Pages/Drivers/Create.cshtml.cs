using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Drivers;

public class CreateModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Driver Driver { get; set; } = new();

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        db.Drivers.Add(Driver);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
