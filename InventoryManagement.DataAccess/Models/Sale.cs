using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataAccess.Models;

public class Sale
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string SaleProduct { get; set; }

    [Required]
    [StringLength(5)]
    public string SaleQuantity { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime SaleDate { get; set; }
}
