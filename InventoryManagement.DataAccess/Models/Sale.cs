using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Models
{
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
}
