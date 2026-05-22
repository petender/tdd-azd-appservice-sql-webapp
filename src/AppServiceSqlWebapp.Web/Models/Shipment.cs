namespace AppServiceSqlWebapp.Web.Models;

public class Shipment
{
    public int Id { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public double Weight { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime EstimatedDelivery { get; set; }
    public int DriverId { get; set; }
    public int WarehouseId { get; set; }

    public Driver? Driver { get; set; }
    public Warehouse? Warehouse { get; set; }
}
