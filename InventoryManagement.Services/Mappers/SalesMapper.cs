using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Mappers;

public class SalesMapper:ISalesMapper
{
    public Sale MapToSales(SalesDTO salesDto)
    {
        return new Sale
        {
            SaleProduct = salesDto.SaleProduct,
            SaleQuantity = salesDto.SaleQuantity,
            SaleDate = salesDto.SaleDate
        };
    }

    public SalesDTO MapToDTO(Sale sale)
    {
        return new SalesDTO
        {
            SaleProduct = sale.SaleProduct,
            SaleQuantity = sale.SaleQuantity,
            SaleDate = sale.SaleDate
        };
    }
}
