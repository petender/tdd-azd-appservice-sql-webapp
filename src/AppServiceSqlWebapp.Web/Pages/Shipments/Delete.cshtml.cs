using AppServiceSqlWebapp.Web.Data;
using AppServiceSqlWebapp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppServiceSqlWebapp.Web.Pages.Shipments;

public class DeleteModel(LogisticsDbContext db) : PageModel
{
    [BindProperty]
    public Shipment Shipment { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var shipment = await db.Shipments.FindAsync(id);
        if (shipment is null)
            return NotFound();

        Shipment = shipment;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var shipment = await db.Shipments.FindAsync(Shipment.Id);
        if (shipment is null)
            return NotFound();

        db.Shipments.Remove(shipment);
        await db.SaveChangesAsync();
        return RedirectToPage("Index");
    }
}
