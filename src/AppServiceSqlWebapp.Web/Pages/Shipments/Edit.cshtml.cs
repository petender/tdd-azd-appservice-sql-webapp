using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AppServiceSqlWebapp.Web.Pages.Shipments;

public class EditModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Shipment Shipment { get; set; } = new();

    public SelectList Drivers { get; set; } = default!;
    public SelectList Warehouses { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var shipment = await db.Shipments.FindAsync(id);
        if (shipment is null)
            return NotFound();

        Shipment = shipment;
        await LoadSelectListsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            return Page();
        }

        db.Attach(Shipment).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }

    private async Task LoadSelectListsAsync()
    {
        Drivers = new SelectList(await db.Drivers.ToListAsync(), "Id", "FullName");
        Warehouses = new SelectList(await db.Warehouses.ToListAsync(), "Id", "Name");
    }
}
