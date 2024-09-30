using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public interface IPurchaseMapper
{
    Purchase MapToPurchase(PurchaseDTO product);
    PurchaseDTO MapToDTO(Purchase product);
}
