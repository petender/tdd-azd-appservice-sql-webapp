namespace AppServiceSqlWebapp.Web.Models;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int CurrentOccupancy { get; set; }
    public string ManagerName { get; set; } = string.Empty;

    public ICollection<Shipment> Shipments { get; set; } = [];
}
