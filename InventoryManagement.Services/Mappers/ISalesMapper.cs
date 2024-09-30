using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public interface ISalesMapper
{
    Sale MapToSales(SalesDTO product);
    SalesDTO MapToDTO(Sale sale);
}
