using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Drivers;

public class DetailsModel(LogisticsDbContext db) : PageModel
{
    public Driver? Driver { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Driver = await db.Drivers.FindAsync(id);
        if (Driver is null)
            return NotFound();

        return Page();
    }
}
