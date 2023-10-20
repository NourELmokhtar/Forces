using Forces.Application.Interfaces.Repositories;
using Forces.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Infrastructure.Repositories
{
    public class InventoryItemRepository : IInventoryItemRepository
    {
        private readonly IRepositoryAsync<InventoryItem, int> _repository;

        public InventoryItemRepository(IRepositoryAsync<InventoryItem, int> repository)
        {
            _repository = repository;
        }

        public async Task<InventoryItem> Add(InventoryItem InventoryItem)
        {
            return await _repository.AddAsync(InventoryItem);
        }

        public async Task<InventoryItem> Delete(InventoryItem InventoryItem)
        {
            await _repository.DeleteAsync(InventoryItem);

            return InventoryItem;
        }

        public Task<List<InventoryItem>> GetAll()
        {
            return _repository.GetAllAsync();
        }

        public Task<InventoryItem> GetById(int Id)
        {
            return _repository.GetByIdAsync(Id);
        }

        public async Task<InventoryItem> Update(InventoryItem InventoryItem)
        {
            await _repository.UpdateAsync(InventoryItem);

            return InventoryItem;
        }
    }
}
