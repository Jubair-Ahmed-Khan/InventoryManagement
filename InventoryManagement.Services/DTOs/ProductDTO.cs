using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services.DTOs
{
    public class ProductDTO
    {

        public string ProductName { get; set; }
        public string ProductQuantity { get; set; }
    }
}
