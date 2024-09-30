using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Services.DTOs;

public class SalesDTO
{
    public string SaleProduct { get; set; }

    public string SaleQuantity { get; set; }

    [DataType(DataType.Date)]
    public DateTime SaleDate { get; set; }
}
