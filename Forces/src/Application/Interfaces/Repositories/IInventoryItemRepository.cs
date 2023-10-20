using Forces.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forces.Application.Interfaces.Repositories
{
    public interface IInventoryItemRepository
    {
        Task<List<InventoryItem>> GetAll();
        Task<InventoryItem> GetById(int Id);
        Task<InventoryItem> Add(InventoryItem InventoryItem);
        Task<InventoryItem> Update(InventoryItem InventoryItem);
        Task<InventoryItem> Delete(InventoryItem InventoryItem);
    }
}
