using InventoryManagement.DataAccess.Models;

namespace InventoryManagement.DataAccess.Contacts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(int id,Product product);
    Task DeleteAsync(int id);
}
