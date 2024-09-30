using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Contacts;

public interface ISalesService
{
	Task<IEnumerable<Sale>> GetAllAsync();
	Task<Sale> GetByIdAsync(int id);
	Task AddAsync(SalesDTO sale);
	Task UpdateAsync(int id, SalesDTO sale);
	Task DeleteAsync(int id);
}
