using System.ComponentModel.DataAnnotations;


namespace InventoryManagement.Services.DTOs;

public class PurchaseDTO
{
    public string PurchaseProduct { get; set; }

    public string PurchaseQuantity { get; set; }

    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; }
}
