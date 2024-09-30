using InventoryManagement.DataAccess.Contacts;
using InventoryManagement.DataAccess.Models;
using InventoryManagement.Services.Contacts;
using InventoryManagement.Services.Mappers;
using InventoryManagement.Services.DTOs;

namespace InventoryManagement.Services.Services;

public class SalesService : ISalesService
{
	private readonly ISalesRepository _repository;
    private readonly ISalesMapper _mapper;

    public SalesService(ISalesRepository repository,ISalesMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<IEnumerable<Sale>> GetAllAsync()
	{
		return await _repository.GetAllAsync();
	}

	public async Task<Sale> GetByIdAsync(int id)
	{
		return await _repository.GetByIdAsync(id);
	}

	public async Task AddAsync(SalesDTO salesDto)
	{
        var newProduct = _mapper.MapToSales(salesDto);
        await _repository.AddAsync(newProduct);
	}

	public async Task UpdateAsync(int id, SalesDTO salesDto)
	{
        var newProduct = _mapper.MapToSales(salesDto);
        await _repository.UpdateAsync(id, newProduct);
    }

	public async Task DeleteAsync(int id)
	{
		await _repository.DeleteAsync(id);
	}
}
