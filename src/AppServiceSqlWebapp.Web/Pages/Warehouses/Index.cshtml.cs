using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Warehouses;

public class IndexModel(LogisticsDbContext db) : PageModel
{
    public List<Warehouse> Warehouses { get; set; } = [];

    public async Task OnGetAsync()
    {
        Warehouses = await db.Warehouses.OrderBy(w => w.Name).ToListAsync();
    }
}
