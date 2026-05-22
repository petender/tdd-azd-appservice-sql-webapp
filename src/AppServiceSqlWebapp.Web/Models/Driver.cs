namespace AppServiceSqlWebapp.Web.Models;

public class Driver
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Status { get; set; } = "Available";
    public string AssignedVehicle { get; set; } = string.Empty;

    public ICollection<Shipment> Shipments { get; set; } = [];
}
