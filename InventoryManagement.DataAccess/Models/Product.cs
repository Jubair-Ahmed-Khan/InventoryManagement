using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataAccess.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string ProductName { get; set; }

    [Required]
    [StringLength(5)]
    public string ProductQuantity { get; set; }
}
