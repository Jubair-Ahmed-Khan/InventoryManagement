using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;

namespace InventoryManagement.Services.Services;

public class SalesService : ISalesService
{
	private readonly ISalesRepository _repository;

	public SalesService(ISalesRepository repository)
	{
		_repository = repository;
	}

	public async Task<IEnumerable<Sale>> GetAllAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<Sale> GetByIdAsync(int id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task AddAsync(Sale sale)
	{
		await _repository.AddAsync(sale);
	}

	public async Task UpdateAsync(int id, Sale sale)
	{
		await _repository.UpdateAsync(id, sale);
	}

	public async Task DeleteAsync(int id)
	{
		await _repository.DeleteAsync(id);
	}
}
