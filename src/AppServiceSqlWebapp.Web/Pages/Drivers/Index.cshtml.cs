using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Drivers;

public class IndexModel(LogisticsDbContext db) : PageModel
{
    public List<Driver> Drivers { get; set; } = [];

    public async Task OnGetAsync()
    {
        Drivers = await db.Drivers.OrderBy(d => d.FullName).ToListAsync();
    }
}
