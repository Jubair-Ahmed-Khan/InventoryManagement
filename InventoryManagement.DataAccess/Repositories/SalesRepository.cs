using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.DataAccess.Repositories;

public class SalesRepository : ISalesRepository
{
	private readonly ApplicationDbContext _context;

	public SalesRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Sale>> GetAllAsync()
	{
		return await _context.Sales.OrderByDescending(p => p.Id).ToListAsync();
	}

	public async Task<Sale> GetByIdAsync(int id)
	{
		return await _context.Sales.Where(p => p.Id == id).FirstOrDefaultAsync();
	}

	public async Task AddAsync(Sale sale)
	{
		await _context.Sales.AddAsync(sale);
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(int id, Sale sale)
	{
		Sale pr = await _context.Sales.Where(p => p.Id == id).FirstOrDefaultAsync();

		pr.SaleProduct = sale.SaleProduct;
		pr.SaleQuantity = sale.SaleQuantity;
		pr.SaleDate = sale.SaleDate;

		await _context.SaveChangesAsync();
	}

	public async Task DeleteAsync(int id)
	{
		Sale sale = await _context.Sales.FindAsync(id);

		if (sale != null)
		{
			_context.Sales.Remove(sale);
			await _context.SaveChangesAsync();
		}
	}
}
