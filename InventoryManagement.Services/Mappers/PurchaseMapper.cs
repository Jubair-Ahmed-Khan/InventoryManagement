using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public class PurchaseMapper:IPurchaseMapper
{
    public Purchase MapToPurchase(PurchaseDTO productDto)
    {
        return new Purchase
        {
            PurchaseProduct = productDto.PurchaseProduct,
            PurchaseQuantity = productDto.PurchaseQuantity,
            PurchaseDate = productDto.PurchaseDate
        };
    }

    public PurchaseDTO MapToDTO(Purchase product)
    {
        return new PurchaseDTO
        {
            PurchaseProduct = product.PurchaseProduct,
            PurchaseQuantity = product.PurchaseQuantity,
            PurchaseDate = product.PurchaseDate
        };
    }
}
