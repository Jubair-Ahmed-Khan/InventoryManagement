using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public interface IProductMapper
{
    Product MapToProduct(ProductDTO product);
    ProductDTO MapToDTO(Product product);
}
