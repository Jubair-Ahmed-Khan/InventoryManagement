using InventoryManagement.DataAccess.Models;

namespace InventoryManagement.Services.Contacts;

public interface ISalesService
{
	Task<IEnumerable<Sale>> GetAllAsync();
	Task<Sale> GetByIdAsync(int id);
	Task AddAsync(Sale sale);
	Task UpdateAsync(int id, Sale sale);
	Task DeleteAsync(int id);
}
