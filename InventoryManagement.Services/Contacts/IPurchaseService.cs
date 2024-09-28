using InventoryManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Services.Contacts
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetAllAsync();
        Task<Purchase> GetByIdAsync(int id);
        Task AddAsync(Purchase purchase);
        Task UpdateAsync(int id, Purchase purchase);
        Task DeleteAsync(int id);
    }
}
