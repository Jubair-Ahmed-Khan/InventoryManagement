﻿using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.DataAccess.Models;

public class Purchase
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string PurchaseProduct { get; set; }

    [Required]
    [StringLength(5)]
    public string PurchaseQuantity { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; }
}
