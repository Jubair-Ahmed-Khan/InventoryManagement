using InventoryManagement.DataAccess.Models;

namespace InventoryManagement.DataAccess.Contacts;

public interface ISalesRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<Sale> GetByIdAsync(int id);
    Task AddAsync(Sale sale);
    Task UpdateAsync(int id, Sale sale);
    Task DeleteAsync(int id);
}
