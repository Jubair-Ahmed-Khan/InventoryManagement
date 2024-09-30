using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public class ProductMapper : IProductMapper
{
    public Product MapToProduct(ProductDTO productDto)
    {
        return new Product
        {
            ProductName = productDto.ProductName,
            ProductQuantity = productDto.ProductQuantity
        };
    }

    public ProductDTO MapToDTO(Product product)
    {
        return new ProductDTO
        {
            ProductName = product.ProductName,
            ProductQuantity = product.ProductQuantity,
        };
    }
}
